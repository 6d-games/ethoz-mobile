#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Use a given item.")]
    public class UseItem : FsmStateAction
    {
        public InventoryItemBase item;
        
        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            item.Use();
            Finish();
        }
    }
}

#endif