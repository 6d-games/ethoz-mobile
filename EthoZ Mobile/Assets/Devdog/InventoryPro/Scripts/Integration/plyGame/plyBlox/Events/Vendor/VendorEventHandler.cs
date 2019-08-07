#if PLY_GAME

using System.Collections.Generic;
using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    public class VendorEventHandler : plyEventHandler
    {
        public List<plyEvent> onSoldItemToVendor = new List<plyEvent>();
        public List<plyEvent> onBoughtItemFromVendor = new List<plyEvent>();
        public List<plyEvent> onBoughtItemBackFromVendor = new List<plyEvent>();
        

        public override void StateChanged()
        {
            onSoldItemToVendor.Clear();
            onBoughtItemFromVendor.Clear();
            onBoughtItemBackFromVendor.Clear();
        }

        public override void AddEvent(plyEvent e)
        {
            if (e.uniqueIdent.Equals("Vendor OnSoldItemToVendor"))
                onSoldItemToVendor.Add(e);

            if (e.uniqueIdent.Equals("Vendor OnBoughtItemFromVendor"))
                onBoughtItemFromVendor.Add(e);

            if (e.uniqueIdent.Equals("Vendor OnBoughtItemBackFromVendor"))
                onBoughtItemBackFromVendor.Add(e);
        }

        public override void CheckEvents()
        {
            enabled = onSoldItemToVendor.Count > 0
                      || onBoughtItemFromVendor.Count > 0
                      || onBoughtItemBackFromVendor.Count > 0;
        }


        public void OnSoldItemToVendor(InventoryItemBase item, uint amount, VendorTrigger vendor)
        {
            if (onSoldItemToVendor.Count > 0)
            {
                RunEvents(onSoldItemToVendor, 
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)item.ID),
                    new plyEventArg("amount", (int)amount),
                    new plyEventArg("vendor", vendor));
            }
        }

        public void OnBoughtItemFromVendor(InventoryItemBase item, uint amount, VendorTrigger vendor)
        {
            if (onBoughtItemFromVendor.Count > 0)
            {
                RunEvents(onBoughtItemFromVendor,
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)item.ID),
                    new plyEventArg("amount", (int)amount),
                    new plyEventArg("vendor", vendor));
            }
        }

        public void OnBoughtItemBackFromVendor(InventoryItemBase item, uint amount, VendorTrigger vendor)
        {
            if (onBoughtItemBackFromVendor.Count > 0)
            {
                RunEvents(onBoughtItemBackFromVendor,
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)item.ID),
                    new plyEventArg("amount", (int)amount),
                    new plyEventArg("vendor", vendor));
            }
        }
    }
}

#endif