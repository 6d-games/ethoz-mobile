#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the item's category ID.")]
    public class GetItemCategoryIDFsmObject : FsmStateAction
    {
        public FsmObject obj;
        public FsmInt result;

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

            result.Value = (int)item.category.ID;
            Finish();
        }
    }
}

#endif