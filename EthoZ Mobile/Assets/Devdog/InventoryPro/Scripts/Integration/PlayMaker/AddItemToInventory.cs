#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Adds an item in the inventory, and if you have multiple inventories it will select the best inventory for the item.")]
    public class AddItemToInventory : FsmStateAction
    {
        //public ItemCollectionBase collection;
        public InventoryItemBase item;
        public FsmBool overwriteAmount;
        public FsmInt amount = 1;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            if(overwriteAmount.Value)
            {
                item.currentStackSize = (uint)amount.Value;
            }

            InventoryManager.AddItem(item);

            if(item.IsInstanceObject() == false)
            {
                item.currentStackSize = 1;
            }
            
            Finish();
        }
    }
}

#endif