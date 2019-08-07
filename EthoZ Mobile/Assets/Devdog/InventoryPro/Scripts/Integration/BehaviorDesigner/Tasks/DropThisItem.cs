#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory(InventoryPro.ProductName)]
    [TaskDescription("Use a given item.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class DropThisItem : Action
    {
        protected InventoryItemBase item;

        public override void OnAwake()
        {
            base.OnAwake();

            item = this.Owner.GetComponent<InventoryItemBase>();
        }

        public override TaskStatus OnUpdate()
        {
            item.itemCollection[item.index].TriggerDrop();
            return TaskStatus.Success;
        }
    }
}

#endif