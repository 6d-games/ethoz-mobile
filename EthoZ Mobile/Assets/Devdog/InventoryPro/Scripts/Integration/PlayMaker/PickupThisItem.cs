#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Loots this item.")]
    public class PickupThisItem : FsmStateAction
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
            item.PickupItem();
            Finish();
        }
    }
}

#endif