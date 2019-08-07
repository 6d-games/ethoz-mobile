#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Use a given item.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class UseItem : Action
    {
        public InventoryItemBase item;

        public override TaskStatus OnUpdate()
        {
            if (item == null) {
                return TaskStatus.Failure;
            }
            item.Use();
            return TaskStatus.Success;
        }
        
        public override void OnReset()
        {
            item = null;
        }
    }
}

#endif