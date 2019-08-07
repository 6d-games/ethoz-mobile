#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Set items in a collection, replaces the old items that were in the collection before it.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class SetItemsCollection : Action
    {
        public InventoryItemBase[] items;
        public ItemCollectionBase collection;

        public override TaskStatus OnUpdate()
        {
            if (collection == null) {
                return TaskStatus.Failure;
            }
            collection.SetItems(items, true);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            items = null;
            collection = null;
        }
    }
}

#endif