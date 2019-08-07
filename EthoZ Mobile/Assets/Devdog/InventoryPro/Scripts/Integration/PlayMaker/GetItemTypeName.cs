#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the item type's name.")]
    public class GetItemTypeName : FsmStateAction
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
            result.stringValue = item.GetType().ToString();

            Finish();
        }
    }
}

#endif