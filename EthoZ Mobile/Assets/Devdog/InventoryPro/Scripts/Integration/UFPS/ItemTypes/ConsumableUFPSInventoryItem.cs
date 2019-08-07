#if UFPS

using Devdog.General;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.UFPS
{
    [RequireComponent(typeof(ItemTriggerUFPS))]
    public partial class ConsumableUFPSInventoryItem : UFPSInventoryItemBase
    {
        public General.AudioClipInfo pickupSound;

        public float restoreHealthAmount = 10f;


        public override GameObject Drop()
        {
            return UFPSDrop((int)currentStackSize);
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

        public override int Use()
        {
            var used = base.Use();
            if (used < 0)
            {
                return used;
            }

            var dmgHandler = PlayerManager.instance.currentPlayer.gameObject.GetComponent<vp_FPPlayerDamageHandler>();
            if (dmgHandler != null)
            {
                dmgHandler.CurrentHealth += restoreHealthAmount;
            }

            currentStackSize--;
            if (itemCollection != null)
            {
                if (currentStackSize <= 0)
                {
                    itemCollection.NotifyItemRemoved(this, ID, index, 1);
                    itemCollection.SetItem(index, null, true);
                }

                itemCollection[index].Repaint();
            }

//            NotifyItemUsed(1, true);
            return 1;
        }
    }
}

#endif