#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Set items on a treasure chest, replaces the old items that were in the collection before it.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class SetItemsTreasureChest : Action
    {
        public InventoryItemBase[] items;
        public LootableObject chest;

        public override TaskStatus OnUpdate()
        {
            if (chest == null) {
                return TaskStatus.Failure;
            }
            chest.items = items;
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            items = null;
            chest = null;
        }
    }
}

#endif