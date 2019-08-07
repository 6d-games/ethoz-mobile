#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the item's category name.")]
    public class GetItemCategoryName : FsmStateAction
    {
        public InventoryItemBase item;
        
        [UIHint(UIHint.Variable)]
        public FsmVar result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            result.stringValue = item.category.name;

            Finish();
        }
    }
}

#endif