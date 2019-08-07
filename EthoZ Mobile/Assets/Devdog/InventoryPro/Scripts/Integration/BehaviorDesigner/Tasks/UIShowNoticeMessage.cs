#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Show a notice message")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class UIShowNoticeMessage : Action
    {
        public SharedString title;
        public SharedString message;
        public SharedColor color;
        public NoticeDuration duration = NoticeDuration.Medium;
        public InventoryItemBase item;

        public override TaskStatus OnUpdate()
        {
            var m = new InventoryNoticeMessage(title.Value, message.Value, duration, color.Value);

            if (item != null)
                m.Show(item.name, item.description);
            else
                m.Show();

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            title = "";
            message = "";
            color = Color.black;
            duration = NoticeDuration.Medium;
            item = null;
        }
    }
}

#endif