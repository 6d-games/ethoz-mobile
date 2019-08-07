#if PLAYMAKER

using HutongGames.PlayMaker;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Change currency of a collection.")]
    public class ChangeCurrencyCollection : FsmStateAction
    {
        //public ItemCollectionBase collection;
        public FsmInt currencyID;
        public FsmFloat amount = 1f;
        public ItemCollectionBase collection;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            if (amount.Value > 0f)
            {
                collection.AddCurrency((uint)currencyID.Value, amount.Value);
            }
            else if (amount.Value < 0f)
            {
                collection.RemoveCurrency((uint)currencyID.Value, Mathf.Abs(amount.Value), true);
            }

            Finish();
        }
    }
}

#endif