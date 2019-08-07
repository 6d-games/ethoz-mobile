#if PLAYMAKER

using Devdog.General.UI;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Show or hide a dialog")]
    public class UIShowHideUIWindow : FsmStateAction
    {
        public FsmBool show;
        public UIWindow window;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            if (show.Value)
                window.Show();
            else
                window.Hide();

            Finish();
        }
    }
}

#endif