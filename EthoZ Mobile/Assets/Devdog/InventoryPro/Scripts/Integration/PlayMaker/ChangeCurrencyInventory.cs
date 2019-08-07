#if PLAYMAKER

using HutongGames.PlayMaker;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Change currency of the inventory.")]
    public class ChangeCurrencyInventory : FsmStateAction
    {
        //public ItemCollectionBase collection;
        public FsmInt currencyID;
        public FsmFloat amount = 1f;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            if (amount.Value > 0f)
            {
                InventoryManager.AddCurrency((uint)currencyID.Value, amount.Value);
            }
            else if (amount.Value < 0f)
            {
                InventoryManager.RemoveCurrency((uint)currencyID.Value, Mathf.Abs(amount.Value));
            }

            Finish();
        }
    }
}

#endif