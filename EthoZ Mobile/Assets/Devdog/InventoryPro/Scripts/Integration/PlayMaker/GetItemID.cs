#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the item's (unique) ID.")]
    public class GetItemID : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        public InventoryItemBase item;
        
        public FsmInt result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            result.Value = (int)item.ID;

            Finish();
        }
    }
}

#endif