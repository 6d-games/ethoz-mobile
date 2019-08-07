#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Loots this item.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class PickupThisItem : Action
    {
        protected InventoryItemBase item;

        public override TaskStatus OnUpdate()
        {
            item.PickupItem();
            return TaskStatus.Success;
        }

        public override void OnAwake()
        {
            base.OnAwake();

            item = this.Owner.GetComponent<InventoryItemBase>();
        }
    }
}

#endif