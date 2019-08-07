#if UFPS_MULTIPLAYER

using System.Collections;
using System.Collections.Generic;
using Devdog.General;
using Devdog.General.ThirdParty.UniLinq;
using UnityEngine.Assertions;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.UFPS.Multiplayer
{
    [UnityEngine.RequireComponent(typeof(LootableObject))]
    [UnityEngine.RequireComponent(typeof(PhotonView))]
    [UnityEngine.AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/UFPS/PhotonLootableObjectSyncer")]
    public partial class PhotonLootableObjectSyncer : Photon.MonoBehaviour, ITriggerCallbacks
    {
        private LootableObject _lootable { get; set; }
        private TriggerBase _trigger { get; set; }

        private List<PhotonPlayer> _activeUsers = new List<PhotonPlayer>();

        protected virtual void Awake()
        {
            _lootable = GetComponent<LootableObject>();
            _lootable.OnLootedItem += LootableOnOnLootedItem;

            _trigger = GetComponent<TriggerBase>();
        }

        protected virtual IEnumerator Start()
        {
            yield return null;
            Init();
        }

        protected virtual void Init()
        {
            // Everytime the items are generated the clients should request the new item list + index them with UFPS (InventoryMPUFPSPickupManager.instance.RegisterPickup)
            // otherwise replication will fail.
            DevdogLogger.LogVerbose("Requesting item list (initial) - (isMaster: " + PhotonNetwork.isMasterClient + ")");
            photonView.RPC("RequestLootableItems", PhotonTargets.MasterClient);
        }

        public bool OnTriggerUsed(Player player)
        {
            if (PhotonNetwork.isMasterClient == false)
            {
                _lootable.items = new InventoryItemBase[0]; // Clear the items, wait for the server to respond with the item list.
            }

            // Make sure we're up to date.
            DevdogLogger.LogVerbose("Client used trigger -> Notify other clients + Requesting item list (isMaster: " + PhotonNetwork.isMasterClient + ")", player);
            photonView.RPC("RequestLootableItems", PhotonTargets.MasterClient);
            photonView.RPC("OnTriggerUsedByOtherClient", PhotonTargets.Others);

            return false;
        }

        public bool OnTriggerUnUsed(Player player)
        {
            DevdogLogger.LogVerbose("Client un-used trigger -> Notify other clients", player);
            photonView.RPC("OnTriggerUnUsedByOtherClient", PhotonTargets.Others);

            return false;
        }


        [PunRPC]
        protected void OnTriggerUsedByOtherClient(PhotonMessageInfo info)
        {
            _trigger.DoVisuals();

            if (PhotonNetwork.isMasterClient)
            {
                _activeUsers.Add(info.sender);
            }
        }

        [PunRPC]
        protected void OnTriggerUnUsedByOtherClient(PhotonMessageInfo info)
        {
            _trigger.UndoVisuals();

            if (PhotonNetwork.isMasterClient)
            {
                _activeUsers.Remove(info.sender);
            }
        }

        [PunRPC]
        protected void SetLootableItems(string itemIDsString, PhotonMessageInfo info)
        {
            Assert.IsTrue(info.sender.ID == PhotonNetwork.masterClient.ID, "SetLootableItems didn't come from masterClient!");

            // Delete the old items.
            foreach (var item in _lootable.items)
            {
                if(item == null)
                {
                    continue;
                }

                Destroy(item.gameObject);
            }

            if (string.IsNullOrEmpty(itemIDsString))
            {
                _lootable.items = new InventoryItemBase[0];
                return;
            }

            // Set the lootable items for this object.
            _lootable.items = GetItemsFromNetworkingString(itemIDsString);

            // Update loot window
            _lootable.lootUI.Clear(false);
            _lootable.lootUI.SetItems(_lootable.items, true);
            foreach (var cur in _lootable.currencies)
            {
                _lootable.lootUI.AddCurrency(cur.currency, cur.amount);
            }
        }

        private void LootableOnOnLootedItem(InventoryItemBase item, uint itemId, uint slot, uint amount)
        {
            DevdogLogger.LogVerbose("[Client] LootableObject - Item got removed from LootableObject", this);
            photonView.RPC("LootableObjectItemRemoved", PhotonTargets.OthersBuffered, (int)slot);

            vp_MPNetworkPlayer player;
            if (!vp_MPNetworkPlayer.Players.TryGetValue(PlayerManager.instance.currentPlayer.transform, out player))
            {
                DevdogLogger.LogWarning("Player could not be found...");
                return;
            }

            var ufpsItem = item as UFPSInventoryItemBase;
            if(ufpsItem != null)
            {
                InventoryMPUFPSPickupManager.instance.photonView.RPC("InventoryPlayerPickedUpItem", PhotonTargets.Others, ufpsItem.itemPickup.ID, player.ID, transform.position, transform.rotation);
            }
        }

        [PunRPC]
        private void LootableObjectItemRemoved(int slot, PhotonMessageInfo info)
        {
            DevdogLogger.LogVerbose("[Client] LootableObject - Other client looted slot: " + slot, transform);
            if (slot < 0 || slot >= _lootable.items.Length)
            {
                UnityEngine.Debug.LogWarning("Item is out of range " + slot + " can't set item");
                return;
            }
            
            _lootable.items[slot] = null;
            if (_lootable.lootUI.items.Length == _lootable.items.Length)
            {
                _lootable.lootUI.SetItem((uint)slot, null, true);
            }
        }

        [PunRPC]
        private void RequestLootableItems(PhotonMessageInfo info)
        {
            DevdogLogger.LogVerbose("[Server] LootableObject - Client requested LootableObject item list", this);

            string result = GetItemsAsNetworkString();
            photonView.RPC("SetLootableItems", info.sender, result);
        }

        private InventoryItemBase[] GetItemsFromNetworkingString(string itemIDsString)
        {
            string[] itemIDs = itemIDsString.Split(',');
            var items = new InventoryItemBase[itemIDs.Length];

            for (int i = 0; i < itemIDs.Length; i++)
            {
                if (itemIDs[i].Trim().StartsWith("-1"))
                {
                    items[i] = null;
                    continue;
                }

                var x = itemIDs[i].Split(':');
                var item = Instantiate<InventoryItemBase>(ItemManager.database.items[int.Parse(x[0])]);
                item.currentStackSize = uint.Parse(x[1]);

                item.transform.SetParent(transform);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.identity;
                item.gameObject.SetActive(false);

                if (InventoryMPUFPSPickupManager.instance != null)
                {
                    var ufpsItem = item as UFPSInventoryItemBase;
                    if (ufpsItem != null)
                    {
                        InventoryMPUFPSPickupManager.instance.RegisterPickup(ufpsItem);
                    }
                }

                items[i] = item;
            }

            return items;
        }

        private string GetItemsAsNetworkString()
        {
            // Master defines the items to be looted, and sends it to the clients.
            return string.Join(",", _lootable.items.Select(x => x == null ? ("-1:0") : (x.ID.ToString() + ":" + x.currentStackSize.ToString())).ToArray()); // Concat as a string, because photon is being bitchy about int arrays
        }
    }
}

#endif