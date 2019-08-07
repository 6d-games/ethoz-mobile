#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Inventories", "Inventoris OnSwappedItems", Description = "Called when 2 items were swapped, this can be called between 2 different collections. <b>Note that it can only be used on the player.</b>" + "\n\n" +
        "<b>Temp variables:</b>\n\n" +
        "- fromCollection (ItemCollectionBase)\n" +
        "- fromSlot (int)\n" +
        "- toCollection (ItemCollectionBase)\n" +
        "- toSlot (int)")]
    public class InventoriesOnSwappedItems : plyEvent
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