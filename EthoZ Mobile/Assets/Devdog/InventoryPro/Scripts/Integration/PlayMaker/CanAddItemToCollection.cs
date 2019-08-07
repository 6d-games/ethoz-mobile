#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Check if the given item can be added to the collection.")]
    public class CanAddItemToCollection : FsmStateAction
    {
        public InventoryItemBase item;

        public FsmObject collection;

        [UIHint(UIHint.Variable)]
        public FsmVar result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var col = collection.Value as ItemCollectionBase;
            if (col == null)
            {
                Finish();
                return;
            }

            result.boolValue = col.CanAddItem(item);

            Finish();
        }
    }
}

#endif