#if BEHAVIOR_DESIGNER

using Devdog.General.UI;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Show or hide a dialog")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class UIShowHideUIWindow : Action
    {
        public SharedBool show;
        public UIWindow window;

        public override TaskStatus OnUpdate()
        {
            if (window == null) {
                return TaskStatus.Failure;
            }
            if (show.Value)
                window.Show();
            else
                window.Hide();

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            show = false;
            window = null;
        }
    }
}

#endif