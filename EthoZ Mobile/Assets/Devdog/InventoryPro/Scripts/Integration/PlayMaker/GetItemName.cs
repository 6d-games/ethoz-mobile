#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the item's name.")]
    public class GetItemName : FsmStateAction
    {
        public InventoryItemBase item;
        public FsmString result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            result.Value = item.name;

            Finish();
        }
    }
}

#endif