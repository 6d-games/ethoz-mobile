#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Collection", "Collection OnUnstackedItemCollectionFull", Description = "Called when an item is unstacked in this collection, but it was full and therefore the action was aborted. <b>Note that it can only be used on a collection.</b>" + "\n\n" +
        "<b>Temp variables:</b>\n\n" +
        "- fromSlot (int)\n" +
        "- toSlot (int)\n" +
        "- amount (int)")]
    public class CollectionOnUnstackedItemCollectionFull : plyEvent
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