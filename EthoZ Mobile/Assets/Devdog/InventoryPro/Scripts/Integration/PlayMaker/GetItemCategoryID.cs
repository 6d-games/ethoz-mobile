#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the item's category ID.")]
    public class GetItemCategoryID : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        public InventoryItemBase item;
        
        [UIHint(UIHint.Variable)]
        public FsmVar result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            result.intValue = (int)item.category.ID;
            Finish();
        }
    }
}

#endif