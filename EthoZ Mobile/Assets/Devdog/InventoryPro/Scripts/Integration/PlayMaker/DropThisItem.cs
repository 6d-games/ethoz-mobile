#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory(InventoryPro.ProductName)]
    [HutongGames.PlayMaker.Tooltip("Use a given item.")]
    public class DropThisItem : FsmStateAction
    {

        protected InventoryItemBase item;

        public override void Awake()
        {
            base.Awake();

            item = this.Owner.GetComponent<InventoryItemBase>();
        }

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            item.itemCollection[item.index].TriggerDrop();
            Finish();
        }
    }
}

#endif