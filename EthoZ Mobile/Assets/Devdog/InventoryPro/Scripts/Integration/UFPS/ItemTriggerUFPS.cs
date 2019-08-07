#if UFPS

using System.Collections;
using Devdog.General;
using UnityEngine;
#if UFPS_MULTIPLAYER

using Devdog.InventoryPro.Integration.UFPS.Multiplayer;

#endif

namespace Devdog.InventoryPro.Integration.UFPS
{
    [UnityEngine.AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/UFPS/ItemTriggerUFPS")]
    public class ItemTriggerUFPS : vp_ItemPickup, ITriggerCallbacks
    {
        protected override void Awake()
        {
            m_Item = new vp_ItemPickup.ItemSection();

            var equippable = GetComponent<EquippableUFPSInventoryItem>();
            if (equippable != null)
                m_Item.Type = equippable.itemType;
            else
            {
                var unitType = GetComponent<UnitTypeUFPSInventoryItem>();
                if (unitType != null)
                    m_Item.Type = unitType.unitType;
            }
        }

        protected virtual IEnumerator Start()
        {
#if UFPS_MULTIPLAYER

            yield return null;
            if(isActiveAndEnabled)
            {
                var ufpsItem = GetComponent<UFPSInventoryItemBase>();
                if (InventoryMPUFPSPickupManager.instance != null && ufpsItem != null)
                {
                    InventoryMPUFPSPickupManager.instance.RegisterPickup(ufpsItem);
                }
            }
#else
            yield return null;
#endif
        }


        public bool OnTriggerUsed(Player generalPlayer)
        {

#if UFPS_MULTIPLAYER

            DevdogLogger.LogVerbose("Player picked up item", generalPlayer);

            vp_MPNetworkPlayer player;
            if (!vp_MPNetworkPlayer.Players.TryGetValue(PlayerManager.instance.currentPlayer.transform, out player))
                return false;

            InventoryMPUFPSPickupManager.instance.photonView.RPC("InventoryPlayerPickedUpItem", PhotonTargets.Others, ID, player.ID, transform.position, transform.rotation);

#endif


            return false;
        }

        public bool OnTriggerUnUsed(Player player)
        {

            return false;
        }


        protected override void OnTriggerEnter(Collider col)
        {
            //base.OnTriggerEnter(col);
        }


        /// <summary>
        /// Directly gives it to the player, bypasses server check.
        /// </summary>
        /// <param name="col"></param>
        /// <param name="amount"></param>
        public void TryGiveToPlayer(Collider col, int amount, bool fireEvents = true)
        {
            //m_Depleted = false;
            this.Amount = amount;

            if(m_Item == null || m_Item.Type == null)
                return;

            if (ItemType == null)
                return;

            //if (!Collider.enabled)
            //    return;

            // only do something if the trigger is still active
            //if (m_Depleted)
            //    return;

            // see if the colliding object was a valid recipient
            if ((m_Recipient.Tags.Count > 0) && !m_Recipient.Tags.Contains(col.gameObject.tag))
                return;

            bool result = false;

            int prevAmount = vp_TargetEventReturn<vp_ItemType, int>.SendUpwards(col, "GetItemCount", m_Item.Type);


            if (ItemType == typeof(vp_ItemType))
                result = vp_TargetEventReturn<vp_ItemType, int, bool>.SendUpwards(col, "TryGiveItem", m_Item.Type, ID);
            else if (ItemType == typeof(vp_UnitBankType))
                result = vp_TargetEventReturn<vp_UnitBankType, int, int, bool>.SendUpwards(col, "TryGiveUnitBank", (m_Item.Type as vp_UnitBankType), Amount, ID);
            else if (ItemType == typeof(vp_UnitType))
                result = vp_TargetEventReturn<vp_UnitType, int, bool>.SendUpwards(col, "TryGiveUnits", (m_Item.Type as vp_UnitType), Amount);
            else if (ItemType.BaseType == typeof(vp_ItemType))
                result = vp_TargetEventReturn<vp_ItemType, int, bool>.SendUpwards(col, "TryGiveItem", m_Item.Type, ID);
            else if (ItemType.BaseType == typeof(vp_UnitBankType))
                result = vp_TargetEventReturn<vp_UnitBankType, int, int, bool>.SendUpwards(col, "TryGiveUnitBank", (m_Item.Type as vp_UnitBankType), Amount, ID);
            else if (ItemType.BaseType == typeof(vp_UnitType))
                result = vp_TargetEventReturn<vp_UnitType, int, bool>.SendUpwards(col, "TryGiveUnits", (m_Item.Type as vp_UnitType), Amount);

            if (fireEvents)
            {
                if (result == true)
                {
                    m_PickedUpAmount = (vp_TargetEventReturn<vp_ItemType, int>.SendUpwards(col, "GetItemCount", m_Item.Type) - prevAmount); // calculate resulting amount given
                    OnSuccess(col.transform);
                }
                else
                {
                    OnFail(col.transform);
                }
            }
        }
    }
}

#endif