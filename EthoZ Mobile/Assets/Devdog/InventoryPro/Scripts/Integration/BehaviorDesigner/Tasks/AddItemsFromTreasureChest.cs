#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Adds all of the items from a treasure chest.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class AddItemsFromThreasureChest : Action
    {
        public LootableObject chest;

        public override TaskStatus OnUpdate()
        {
            if (chest == null) {
                return TaskStatus.Failure;
            }

            for (int i = chest.items.Length - 1; i > -1; --i) {
                InventoryManager.AddItem(chest.items[i]);
                chest.items[i] = null;
            }

            return TaskStatus.Success;
        }
        
        public override void OnReset()
        {
            chest = null;
        }
    }
}

#endif