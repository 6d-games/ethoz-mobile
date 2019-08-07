#if UFPS

using Devdog.General;
using UnityEngine;
#if UFPS_MULTIPLAYER
using Devdog.InventoryPro.Integration.UFPS.Multiplayer;
#endif

namespace Devdog.InventoryPro.Integration.UFPS
{
    [RequireComponent(typeof(ItemTriggerUFPS))]
    public partial class EquippableUFPSInventoryItem : UFPSInventoryItemBase
    {
        public vp_ItemType itemType;
        public General.AudioClipInfo pickupSound;


        public uint currentClipCount { get; set; }
        //public override uint currentStackSize
        //{
        //    get { return currentClipCount; }
        //    set { currentClipCount = value; }
        //}

        public override string name
        {
            get
            {
                if (useUFPSItemData && itemType != null)
                    return itemType.DisplayName;
                else
                    return base.name;
            }
            set { base.name = value; }
        }

        public override string description
        {
            get
            {
                if (useUFPSItemData && itemType != null)
                    return itemType.Description;
                else
                    return base.description;
            }
            set { base.description = value; }
        }

        protected override void Awake()
        {
            base.Awake();

//            currentClipCount = 0;
        }

        public override GameObject Drop()
        {
            return UFPSDrop((int)currentClipCount);
        }


        public override void NotifyItemEquipped(EquippableSlot equipSlot, uint amountEquipped)
        {
            base.NotifyItemEquipped(equipSlot, amountEquipped);

            itemPickup.TryGiveToPlayer(PlayerManager.instance.currentPlayer.GetComponentInChildren<Collider>(), (int)currentClipCount);
        }


        public override void NotifyItemUnEquipped(ICharacterCollection equipTo, uint amountUnEquipped)
        {
            base.NotifyItemUnEquipped(equipTo, amountUnEquipped);

            var item = ufpsInventory.GetItem(itemType) as vp_UnitBankInstance;
            if (item != null)
            {
                int unitCount = item.Count;
                if (unitCount > 0)
                {
                    // Remove from weapon clip
                    item.DoRemoveUnits(9999);
                }

                ufpsInventory.TryRemoveItem(itemType, 0);
                currentClipCount = 0;

                if (unitCount > 0)
                {
                    // Give to inventory
                    ufpsInventory.TryGiveUnits(item.UnitType, unitCount);
                }

                return;
            }

            ufpsInventory.TryRemoveItem(itemType, 0);
            currentClipCount = 0;
        }

        public override bool PickupItem()
        {
            bool pickedUp = base.PickupItem();
            if (pickedUp)
            {
                transform.position = Vector3.zero; // Reset position to avoid the user from looting it twice when reloading (reloading temp. enables the item)
                AudioManager.AudioPlayOneShot(pickupSound);
            }
            return pickedUp;
        }
    }
}

#endif