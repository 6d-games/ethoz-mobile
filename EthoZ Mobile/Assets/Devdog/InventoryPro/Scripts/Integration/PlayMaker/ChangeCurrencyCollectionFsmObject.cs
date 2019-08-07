#if PLAYMAKER

using HutongGames.PlayMaker;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Change currency of a collection.")]
    public class ChangeCurrencyCollectionFsmObject : FsmStateAction
    {
        //public ItemCollectionBase collection;
        public FsmInt currencyID;
        public FsmFloat amount = 1f;
        public FsmObject collection;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var c = collection.Value as ItemCollectionBase;
            if (c == null)
            {
                Debug.Log("Can't add currency, given type is not an ItemCollectionBase type");
                Finish();
                return;
            }

            if (amount.Value > 0f)
            {
                c.AddCurrency((uint)currencyID.Value, amount.Value);
            }
            else if (amount.Value < 0f)
            {
                c.RemoveCurrency((uint)currencyID.Value, Mathf.Abs(amount.Value), true);
            }

            Finish();
        }
    }
}

#endif