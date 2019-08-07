#if PLAYMAKER

using Devdog.General.UI;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Show or hide a dialog")]
    public class UIShowHideUIWindowFsmObject : FsmStateAction
    {
        public FsmBool show;
        public FsmObject window;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var w = window.Value as UIWindow;
            if (w != null)
            {
                if (show.Value)
                    w.Show();
                else
                    w.Hide();

            }

            Finish();
        }
    }
}

#endif