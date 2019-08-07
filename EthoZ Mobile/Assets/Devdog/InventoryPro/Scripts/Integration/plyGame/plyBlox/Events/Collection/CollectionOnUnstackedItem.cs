#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Collection", "Collection OnUnstackedItem", Description = "Called when an item was unstacked in this collection. <b>Note that it can only be used on a collection.</b>" + "\n\n" +
        "<b>Temp variables:</b>\n\n" +
        "- itemID (int)\n" +
        "- fromSlot (int)\n" +
        "- amount (ItemCollectionBase)")]
    public class CollectionOnUnstackedItem : plyEvent
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