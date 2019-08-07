#if UFPS_MULTIPLAYER

using System.Collections.Generic;
using Devdog.General;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.UFPS.Multiplayer
{
    [RequireComponent(typeof(vp_MPPickupManager))]
    public partial class InventoryMPUFPSPickupManager : Photon.MonoBehaviour
    {
        public static InventoryMPUFPSPickupManager instance { get; protected set; }


        protected void Awake()
        {
            instance = this;
        }

        public void InstantiateAndRegisterPickupOnAllClients(UFPSInventoryItemBase item)
        {
            if(item != null)
            {
                RegisterPickup(item);
                photonView.RPC("InstantiateItemAndRegisterPickup", PhotonTargets.OthersBuffered, (int)item.ID, (int)item.currentStackSize, item.transform.position);
            }
        }

        public void RegisterPickup(UFPSInventoryItemBase item)
        {
            item.itemPickup.ID = (vp_Utility.PositionToID(item.itemPickup.transform.position) + ((int)item.ID * 100000));
            RegisterPickup(item.itemPickup);

            DevdogLogger.LogVerbose("Registered item with UFPS multiplayer. New UFPS ID: " + item.itemPickup.ID, item);
        }

        /// <summary>
        /// registers a pickup under a position-based ID
        /// </summary>
        protected virtual void RegisterPickup(vp_ItemPickup p)
        {
            // since a single gameobject may have several vp_Pickup components
            // on them, pickups are stored in a dictionary with id keys and
            // pickup list values. (an example of a multi-pickup object is a
            // grenade, which has both a grenade thrower pickup and an ammo
            // pickup on the same object)

            if (!vp_MPPickupManager.Instance.Pickups.ContainsKey(p.ID))
            {
                // if the pickup does not exist, we add a list with a single pickup
                // under the position based ID

                List<vp_ItemPickup> ip = new List<vp_ItemPickup>();
                ip.Add(p);
                vp_MPPickupManager.Instance.Pickups.Add(p.ID, ip);
                vp_Respawner r = p.GetComponent<vp_Respawner>();
                if ((r != null)
                    && !vp_MPPickupManager.Instance.PickupRespawners.ContainsKey(p.ID))
                {
                    vp_MPPickupManager.Instance.PickupRespawners.Add(p.ID, r);
                    r.m_SpawnMode = vp_Respawner.SpawnMode.SamePosition;
                }
                //else TODO: warn
            }
            else
            {
                // if a pickup already exists with this id, we unpack the list,
                // remove it from the dictionary, add the new pickup and re-add
                // it under the same id

                List<vp_ItemPickup> ip;
                if (vp_MPPickupManager.Instance.Pickups.TryGetValue(p.ID, out ip) && ip != null)
                {
                    vp_MPPickupManager.Instance.Pickups.Remove(p.ID);
                    ip.Add(p);
                    vp_MPPickupManager.Instance.Pickups.Add(p.ID, ip);
                }
            }
            //else TODO: warn

        }


        /// <summary>
        /// When a player has looted an item, it will be send to all other clients to get rid of the no longer valid object.
        /// </summary>
        [PunRPC]
        void InventoryPlayerPickedUpItem(int ufpsItemID, int ufpsPlayerID, Vector3 itemPosition, Quaternion itemRotation, PhotonMessageInfo info)
        {
            List<vp_ItemPickup> pickups;
            if (!vp_MPPickupManager.Instance.Pickups.TryGetValue(ufpsItemID, out pickups))
            {
                DevdogLogger.LogWarning("No local copy of ufps item with ID: " + ufpsItemID + " found, ignoring call");
                return;
            }

            if (pickups.Count > 0 && pickups[0] != null && pickups[0].gameObject != null)
                vp_Utility.Activate(pickups[0].gameObject, false);

            DevdogLogger.LogVerbose("Client looted item with UFPS ID: " + ufpsItemID);
            
            vp_MPNetworkPlayer player;
            if (!vp_MPNetworkPlayer.PlayersByID.TryGetValue(ufpsPlayerID, out player))
            {
                DevdogLogger.LogWarning("Player with playerID : " + ufpsPlayerID + " not found");
                return;
            }

            if (player == null)
                return;

            if (player.Collider == null)
                return;

            foreach (vp_ItemPickup p in pickups)
            {
                var a = p as ItemTriggerUFPS;
                if(a == null)
                {
                    continue;
                }
                
                a.TryGiveToPlayer(player.Collider, p.Amount, false);
                Debug.Log("---- Giving UFPS item to player - ItemID: " + a.ID);
            }
        }


        [PunRPC]
        private void InventoryDroppedObject(int itemID, int ufpsItemID, int amount, Vector3 position, Quaternion rotation, PhotonMessageInfo info)
        {
            DevdogLogger.LogVerbose("Player dropped item with ID: " + itemID, info.photonView);

            List<vp_ItemPickup> pickups;
            if (!vp_MPPickupManager.Instance.Pickups.TryGetValue(ufpsItemID, out pickups))
            {
                DevdogLogger.LogWarning("Player dropped object but no local copy was found: UFPS ID: " + ufpsItemID + " - ItemID: " + itemID);
                return;
            }

            if (pickups[0].gameObject != null)
            {
                var item = pickups[0].gameObject.GetComponent<UFPSInventoryItemBase>();
                if (item == null)
                {
                    Debug.LogWarning("Non UFPS Item was dropped??", pickups[0].gameObject);
                    return;
                }

                item.transform.SetParent(null);
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.gameObject.SetActive(true);


                if (item is EquippableUFPSInventoryItem)
                    ((EquippableUFPSInventoryItem)item).currentClipCount = (uint)amount;
                else if (item is UnitTypeUFPSInventoryItem)
                    ((UnitTypeUFPSInventoryItem)item).unitAmount = (uint)amount;


                var trigger = item.gameObject.GetComponent<ItemTriggerUFPS>();
                if (trigger != null)
                {
                    trigger.ID = ufpsItemID;
                    trigger.Amount = amount;
                }

                DevdogLogger.LogVerbose("Client dropped item #" + item.ID + " (" + item.name + ") with " + amount + " bullets (UFPSID: " + ufpsItemID + ")", item);
            }
        }

        [PunRPC]
        private void InstantiateItemAndRegisterPickup(int itemID, int stackSize, Vector3 position)
        {
            var item = Instantiate<InventoryItemBase>(ItemManager.database.items[itemID]);
            item.currentStackSize = (uint)stackSize;

            item.transform.position = position;
            item.transform.localRotation = Quaternion.identity;
            item.gameObject.SetActive(false);

            var ufpsItem = item as UFPSInventoryItemBase;
            if(ufpsItem != null)
            {
                RegisterPickup(ufpsItem);
            }
        }
    }
}

#endif