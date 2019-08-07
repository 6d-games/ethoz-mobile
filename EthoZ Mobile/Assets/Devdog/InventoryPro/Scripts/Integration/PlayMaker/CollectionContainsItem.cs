#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Check if a given collection contains an item")]
    public class CollectionContainsItem : FsmStateAction
    {
        public InventoryItemBase item;
        public FsmObject collection;

        [UIHint(UIHint.Variable)]
        public FsmBool result;

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

            result.Value = col.GetItemCount(item.ID) > 0;

            Finish();
        }
    }
}

#endif