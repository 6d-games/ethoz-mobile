#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Loots this item.")]
    public class PickupItemFsmObject : FsmStateAction
    {

        public FsmObject obj;

        public override void Awake()
        {
            base.Awake();
        }

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var item = obj.Value as InventoryItemBase;
            if (item == null)
            {
                //                Debug.LogWarning("Item given is not an Inventory Pro item and can't be added to the collection.");
                Finish();
                return;
            }

            item.PickupItem();
            Finish();
        }
    }
}

#endif