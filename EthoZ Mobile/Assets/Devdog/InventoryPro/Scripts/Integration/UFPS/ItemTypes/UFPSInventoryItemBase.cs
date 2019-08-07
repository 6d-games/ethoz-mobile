#if UFPS

using Devdog.General;
using UnityEngine;
#if UFPS_MULTIPLAYER
using Devdog.InventoryPro.Integration.UFPS.Multiplayer;
#endif


namespace Devdog.InventoryPro.Integration.UFPS
{
    [RequireComponent(typeof(ItemTriggerUFPS))]
    public abstract class UFPSInventoryItemBase : EquippableInventoryItem
    {
        public bool useUFPSItemData = true;


        protected vp_PlayerEventHandler ufpsEventHandler
        {
            get
            {
                return PlayerManager.instance.currentPlayer.GetComponent<vp_PlayerEventHandler>();
            }
        }
        protected vp_PlayerInventory ufpsInventory
        {
            get
            {
                return PlayerManager.instance.currentPlayer.GetComponent<vp_PlayerInventory>();
            }
        }

        private ItemTriggerUFPS _itemPickup;
        public ItemTriggerUFPS itemPickup
        {
            get
            {
                if (_itemPickup == null)
                    _itemPickup = GetComponent<ItemTriggerUFPS>();

                return _itemPickup;
            }
        }

        protected virtual void Awake()
        {

//#if UFPS_MULTIPLAYER
//            if (InventoryMPUFPSPickupManager.instance != null)
//                InventoryMPUFPSPickupManager.instance.RegisterPickup(this);
//#endif
        }

        protected virtual GameObject UFPSDrop(int amount)
        {
#if UFPS_MULTIPLAYER

            //var dropObj = base.Drop(location, rotation);
            var dropPos = InventorySettingsManager.instance.settings.itemDropHandler.CalculateDropPosition(this);
            NotifyItemDropped(null);

            //gameObject.SetActive(false);
            if (InventoryMPUFPSPickupManager.instance != null)
                InventoryMPUFPSPickupManager.instance.photonView.RPC("InventoryDroppedObject", PhotonTargets.AllBuffered, (int)ID, itemPickup.ID, amount, dropPos, Quaternion.identity);

            return null;

#else
            return base.Drop();
#endif
        }

        public override bool PickupItem()
        {
            bool pickedUp = base.PickupItem();
            if (pickedUp)
                transform.position = Vector3.zero; // Reset position to avoid the user from looting it twice when reloading (reloading temp. enables the item)

            return pickedUp;
        }
    }
}

#endif