#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Characters", "Characters OnEquippedItem", Description = "Called when the character equipped an item. <b>Note the event is only trigged on the player object.</b>" + "\n\n" +
        "<b>Temp variables:</b>\n\n" +
        "- item (InventoryItemBase)\n" +
        "- itemID (int)\n" +
        "- amount (int)")]
    public class CharacterOnEquippedItem : plyEvent
    {
        public override void Run()
        {
            base.Run();
        }

        public override System.Type HandlerType()
        {
            // here the Event is linked to the correct handler
            return typeof(CharactersEventHandler);
        }
    }
}

#endif