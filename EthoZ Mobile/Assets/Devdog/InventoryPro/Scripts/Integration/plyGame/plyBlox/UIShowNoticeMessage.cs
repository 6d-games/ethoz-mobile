#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "UI", "Show notice message", BlockType.Action, ReturnValueType = typeof(UIShowNoticeMessage))]
    public class UIShowNoticeMessage : plyBlock
    {
        [plyBlockField("Message title", ShowName = true, ShowValue = true, DefaultObject = typeof(String_Value))]
        public String_Value title;
        
        [plyBlockField("Message", ShowName = true, ShowValue = true, DefaultObject = typeof(String_Value))]
        public String_Value message;
        
        [plyBlockField("Color", ShowName = true, ShowValue = true, DefaultObject = typeof(Color_Value))]
        public Color_Value color;

        [plyBlockField("Duration", ShowName = true, ShowValue = true, DefaultObject = typeof(NoticeDuration))]
        public NoticeDuration duration;


        public override void Created()
        {
            blockIsValid = message != null;
        }

        public override BlockReturn Run(BlockReturn param)
        {
            var msg = new InventoryNoticeMessage(title.value, message.value, duration, color);
            msg.Show();

            return BlockReturn.OK;
        }
    }
}

#endif