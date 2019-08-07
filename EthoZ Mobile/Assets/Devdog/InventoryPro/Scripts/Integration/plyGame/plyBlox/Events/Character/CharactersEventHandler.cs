#if PLY_GAME

using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    public class CharactersEventHandler : plyEventHandler
    {
        public List<plyEvent> onEquippedItemEvents = new List<plyEvent>();
        public List<plyEvent> onSwappedItemsEvents = new List<plyEvent>();
        public List<plyEvent> onUnEquippedItemEvents = new List<plyEvent>();
        

        public override void StateChanged()
        {
            onEquippedItemEvents.Clear();
            onSwappedItemsEvents.Clear();
            onUnEquippedItemEvents.Clear();
        }

        public override void AddEvent(plyEvent e)
        {
            if (e.uniqueIdent.Equals("Characters OnEquippedItem"))
                onEquippedItemEvents.Add(e);

            if (e.uniqueIdent.Equals("Characters OnSwappedItems"))
                onSwappedItemsEvents.Add(e);

            if (e.uniqueIdent.Equals("Characters OnUnEquippedItem"))
                onUnEquippedItemEvents.Add(e);
        }

        public override void CheckEvents()
        {
            enabled = onEquippedItemEvents.Count > 0
                      || onSwappedItemsEvents.Count > 0
                      || onUnEquippedItemEvents.Count > 0;
        }


        public void CharacterOnEquippedItem(IEnumerable<InventoryItemBase> items, uint amount, bool cameFromCollection)
        {
            if (onEquippedItemEvents.Count > 0)
            {
                var item = items.FirstOrDefault();
                RunEvents(onEquippedItemEvents, 
                    new plyEventArg("item", items.FirstOrDefault()),
                    new plyEventArg("itemID",  item != null ? (int)item.ID : -1),
                    new plyEventArg("amount", (int)amount));
            }
        }

        public void CharacterOnSwappedItems(ItemCollectionBase fromCollection, uint fromSlot, ItemCollectionBase toCollection, uint toSlot)
        {
            if (onSwappedItemsEvents.Count > 0)
            {
                RunEvents(onSwappedItemsEvents,
                    new plyEventArg("fromCollection", fromCollection),
                    new plyEventArg("fromSlot", (int)fromSlot),
                    new plyEventArg("toCollection", toCollection),
                    new plyEventArg("toSlot", (int)toSlot));
            }
        }

        public void CharacterOnUnEquippedItem(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {
            if (onUnEquippedItemEvents.Count > 0)
            {
                RunEvents(onUnEquippedItemEvents,
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)itemID),
                    new plyEventArg("slot", (int)slot),
                    new plyEventArg("amount", (int)amount));
            }
        }
    }
}

#endif