#if BEHAVIOR_DESIGNER

using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Use a given item.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class UseThisItem : Action
    {
        public override TaskStatus OnUpdate()
        {
            this.Owner.SendMessage("Use", SendMessageOptions.RequireReceiver);
            return TaskStatus.Success;
        }
    }
}

#endif