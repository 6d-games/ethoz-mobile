#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Crafting", "Crafting OnCraftingProgress", Description = "Called when the user sells an object to a vendor." + "\n\n" +
        "<b>Temp variables:</b>\n\n" +
        "- itemID (int)\n" +
        "- category (InventoryCraftingCategory)\n" +
        "- categoryID (int)" +
        "- blueprint (InventoryCraftingBlueprint)\n" +
        "- blueprintID (int)")]
    public class CraftingOnCraftingProgress : plyEvent
    {
        public override void Run()
        {
            base.Run();
        }

        public override System.Type HandlerType()
        {
            // here the Event is linked to the correct handler
            return typeof(CraftingEventHandler);
        }
    }
}

#endif