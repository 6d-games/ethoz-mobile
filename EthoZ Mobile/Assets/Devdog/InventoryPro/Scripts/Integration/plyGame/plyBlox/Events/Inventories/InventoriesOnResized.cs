#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Inventories", "Inventories OnResized", Description = "Called when this collection is resized (can either be grown or shrunk). <b>Note that it can only be used on the player.</b>" + "\n\n" +
        "<b>Temp variables:</b>\n\n" +
        "- fromSize (int)\n" +
        "- toSize (int)")]
    public class InventoriesOnResized : plyEvent
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