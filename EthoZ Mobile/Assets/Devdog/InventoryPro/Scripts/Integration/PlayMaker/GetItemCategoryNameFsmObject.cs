#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the item's category name.")]
    public class GetItemCategoryNameFsmObject : FsmStateAction
    {
        public FsmObject obj;
        
        public FsmString result;

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

            result.Value = item.category.name;
            Finish();
        }
    }
}

#endif