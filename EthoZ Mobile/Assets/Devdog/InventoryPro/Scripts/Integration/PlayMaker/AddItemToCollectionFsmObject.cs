#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Adds an item in the inventory, and if you have multiple inventories it will select the best inventory for the item.")]
    public class AddItemToCollectionFsmObject : FsmStateAction
    {
        public FsmObject obj;
        public FsmBool overwriteAmount;
        public FsmInt amount = 1;
        public FsmObject collection;

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

            var col = collection.Value as ItemCollectionBase;
            if (col == null)
            {
                Finish();
                return;
            }

            if(overwriteAmount.Value)
            {
                item.currentStackSize = (uint)amount.Value;
            }

            col.AddItem(item);

            if(item.IsInstanceObject() == false)
            {
                item.currentStackSize = 1;
            }
            
            Finish();
        }
    }
}

#endif