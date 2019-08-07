#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Collection", "Collection OnUsedItem", Description = "Called when an item is used from inside this collection. <b>Note that it can only be used on a collection.</b>" + "\n\n" +
        "<b>Temp variables:</b>\n\n" +
        "- item (InventoryItemBase)\n" +
        "- itemID (int)\n" +
        "- slot (int)\n" +
        "- amount (int)")]
    public class CollectionOnUsedItem : plyEvent
    {
        public override void Run()
        {
            base.Run();
        }

        public override System.Type HandlerType()
        {
            // here the Event is linked to the correct handler
            return typeof(CollectionEventHandler);
        }
    }
}

#endif