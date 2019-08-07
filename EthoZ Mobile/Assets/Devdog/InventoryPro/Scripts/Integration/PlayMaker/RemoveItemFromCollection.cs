#if PLAYMAKER

using UnityEngine;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Remove an item from a collection.")]
    public class RemoveItemFromCollection : FsmStateAction
    {
        public InventoryItemBase item;
        public FsmInt amount = 1;
        public FsmObject collection;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var col = collection.Value as ItemCollectionBase;
            if (col == null)
            {
                Debug.LogWarning("Given collection object is not a ItemCollectionBase");
                Finish();
                return;
            }

            col.RemoveItem(item.ID, (uint)amount.Value);
            Finish();
        }
    }
}

#endif