#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyEvent("Inventory Pro/Vendors", "Vendor OnBoughtItemFromVendor", Description = @"Called when the user buys and item from a vendor. Event can either be used on the vendor triggerHandler, vendorUI or the player" + "\n\n" +
        "<b>Temp variables:</b>\n\n" + 
        "- item (InventoryItemBase)\n" + 
        "- itemID (int)\n" + 
        "- amount (int)\n" +
        "- vendor (Vendor)")]
    public class VendorOnBoughtItemFromVendor : plyEvent
    {
        public override void Run()
        {
            base.Run();
        }

        public override System.Type HandlerType()
        {
            // here the Event is linked to the correct handler
            return typeof(VendorEventHandler);
        }
    }
}

#endif