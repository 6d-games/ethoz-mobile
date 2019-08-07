#if PLY_GAME

using Devdog.General.UI;
using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "UI", "Show hide window", BlockType.Action, ReturnValueType = typeof(UIShowHideUIWindow))]
    public class UIShowHideUIWindow : plyBlock
    {
        [plyBlockField("Window", ShowName = true, ShowValue = true, DefaultObject = typeof (UIWindow), EmptyValueName = "-error-", SubName = "UIWindow", Description = "The window you wish to manipulate.")]
        public UIWindow window;

        [plyBlockField("Action", ShowName = true, ShowValue = true, DefaultObject = typeof(Bool_Value), SubName = "Action", Description = "Action to perform.")]
        public Bool_Value action;


        public override void Created()
        {
            blockIsValid = window != null;
        }

        public override BlockReturn Run(BlockReturn param)
        {
            if (action.value)
                window.Show();
            else
                window.Hide();

            return BlockReturn.OK;
        }
    }
}

#endif