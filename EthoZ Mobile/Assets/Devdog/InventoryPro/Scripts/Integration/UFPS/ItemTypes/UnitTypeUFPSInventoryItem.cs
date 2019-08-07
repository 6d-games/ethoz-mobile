#if UFPS

using Devdog.General;
using Devdog.General.ThirdParty.UniLinq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.InventoryPro.Integration.UFPS
{
    [RequireComponent(typeof(ItemTriggerUFPS))]
    public partial class UnitTypeUFPSInventoryItem : UFPSInventoryItemBase
    {
        public vp_UnitType unitType;
        public uint unitAmount = 1;
        public General.AudioClipInfo pickupSound;

        public override uint currentStackSize
        {
            get { return unitAmount; }
            set { unitAmount = value; }
        }

        public override string name
        {
            get
            {
                if (useUFPSItemData && unitType != null)
                    return unitType.DisplayName;

                return base.name;
            }
            set { base.name = value; }
        }

        public override string description
        {
            get
            {
                if (useUFPSItemData && unitType != null)
                    return unitType.Description;
                
                return base.description;
            }
            set { base.description = value; }
        }
        
        public override GameObject Drop()
        {
            return UFPSDrop((int)currentStackSize);
        }

        public override void NotifyItemEquipped(EquippableSlot equipSlot, uint amountEquipped)
        {
            base.NotifyItemEquipped(equipSlot, amountEquipped);

            AddAmmo(amountEquipped);
            ufpsEventHandler.Register(this); // Enable UFPS events
        }

        public override void NotifyItemUnEquipped(ICharacterCollection equipTo, uint amountUnEquipped)
        {
            base.NotifyItemUnEquipped(equipTo, amountUnEquipped);

            RemoveAmmo(currentStackSize);
            ufpsEventHandler.Unregister(this); // Disable UFPS events
        }


        //// UFPS EVENT
        protected virtual void OnStop_Reload()
        {
            Debug.Log("UFPS event after reload");
            UpdateAmmoAfterUFPSAction();
        }

        // UFPS Event
        protected virtual void OnStop_Attack()
        {
            Debug.Log("UFPS event after fired.");
            UpdateAmmoAfterUFPSAction();
        }

        protected virtual void AddAmmo(uint amount)
        {
            ufpsInventory.SetUnitCount(unitType, (int)amount);
//            UpdateAmmoAfterUFPSAction();
        }

        protected virtual void RemoveAmmo(uint amount)
        {
            int tempCurrentStackSize = (int)amount;
            int bankCount = ufpsInventory.GetUnitCount(unitType);
            if (bankCount > 0)
            {
                ufpsInventory.TryRemoveUnits(unitType, bankCount);
                tempCurrentStackSize -= bankCount;
            }

            foreach (var bankInstance in ufpsInventory.UnitBankInstances)
            {
                if (bankInstance.UnitType == unitType)
                {
                    if (bankInstance.Count >= tempCurrentStackSize)
                    {
                        bankInstance.TryRemoveUnits(tempCurrentStackSize);
                        tempCurrentStackSize = 0;
                    }
                    else if (bankInstance.Count < tempCurrentStackSize)
                    {
                        // Not enoguh for a full removal, but grab as much as possible
                        tempCurrentStackSize -= bankInstance.Count;
                        bankInstance.TryRemoveUnits(bankInstance.Count);
                    }
                }
            }
        }

        /// <summary>
        /// Resyncs the Inventory Pro variables after an UFPS action.
        /// </summary>
        protected virtual void UpdateAmmoAfterUFPSAction()
        {
            int clipsAmmoCount = ufpsInventory.UnitBankInstances.Where(i => i.UnitType == unitType).Sum(i => i.Count);
            int inventoryAmmoCount = ufpsInventory.GetUnitCount(unitType);

//            Debug.Log("Updating clips count : " + clipsAmmoCount);
//            Debug.Log("Updating inventory count : " + inventoryAmmoCount);
            Assert.IsTrue(clipsAmmoCount + inventoryAmmoCount >= 0);

            currentStackSize = (uint)(clipsAmmoCount + inventoryAmmoCount);
            if (currentStackSize <= 0)
            {
                itemCollection[index].item = null;
            }

            itemCollection[index].Repaint();
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