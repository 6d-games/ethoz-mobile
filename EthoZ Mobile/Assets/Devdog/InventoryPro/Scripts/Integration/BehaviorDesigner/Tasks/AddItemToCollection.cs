#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Adds an item in the inventory, and if you have multiple inventories it will select the best inventory for the item.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class AddItemToCollection : Action
    {
        public InventoryItemBase item;
        public SharedInt amount = 1;

        public ItemCollectionBase collection;

        public override TaskStatus OnUpdate()
        {
            if (item == null || collection == null) {
                return TaskStatus.Failure;
            }
            item.currentStackSize = (uint)amount.Value;
            collection.AddItem(item);
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            item = null;
            amount = 1;
            collection = null;
        }
    }
}

#endif