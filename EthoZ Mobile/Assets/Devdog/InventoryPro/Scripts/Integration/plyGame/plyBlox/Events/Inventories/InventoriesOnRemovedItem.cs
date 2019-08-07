#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Inventories", "Inventories OnRemovedItem", Description = "Called when an item is removed from this collection. <b>Note that it can only be used on the player.</b>" + "\n\n" +
        "<b>Temp variables:</b>\n\n" +
        "- item (InventoryItemBase)\n" +
        "- itemID (int)\n" +
        "- slot (int)\n" +
        " - amount (int)")]
    public class InventoriesOnRemovedItem : plyEvent
    {
        public override void Run()
        {
            base.Run();
        }

        public override System.Type HandlerType()
        {
            // here the Event is linked to the correct handler
            return typeof(InventoriesEventHandler);
        }
    }
}

#endif