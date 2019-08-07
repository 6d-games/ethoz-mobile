#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Collection", "Collection OnSorted", Description = "Called when this collection is sorted. <b>Note that it can only be used on a collection.</b>" + "\n\n" +
        "<b>Temp variables:</b>\n\n")]
    public class CollectionOnSorted : plyEvent
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