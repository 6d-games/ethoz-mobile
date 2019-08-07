#if PLY_GAME

using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    public class CollectionEventHandler : plyEventHandler
    {
        public List<plyEvent> onAddedItemsEvents = new List<plyEvent>();
        public List<plyEvent> onUsedReferenceEvents = new List<plyEvent>();
        public List<plyEvent> onUsedItemEvents = new List<plyEvent>();
        public List<plyEvent> onUnstackedItemCollectionFullEvents = new List<plyEvent>();
        public List<plyEvent> onUnstackedItemEvents = new List<plyEvent>();
        public List<plyEvent> onSwappedItemsEvents = new List<plyEvent>();
        public List<plyEvent> onSortedEvents = new List<plyEvent>();
        public List<plyEvent> onResizedEvents = new List<plyEvent>();
        public List<plyEvent> onRemovedReferenceEvents = new List<plyEvent>();
        public List<plyEvent> onRemovedItemEvents = new List<plyEvent>();
        public List<plyEvent> onDroppedItemEvents = new List<plyEvent>();
        public List<plyEvent> onAddedItemCollectionFullEvents = new List<plyEvent>();
        

        public override void StateChanged()
        {
            onAddedItemsEvents.Clear();
            onUsedReferenceEvents.Clear();
            onUsedItemEvents.Clear();
            onUnstackedItemCollectionFullEvents.Clear();
            onUnstackedItemEvents.Clear();
            onSwappedItemsEvents.Clear();
            onSortedEvents.Clear();
            onResizedEvents.Clear();
            onRemovedReferenceEvents.Clear();
            onRemovedItemEvents.Clear();
            onDroppedItemEvents.Clear();
            onAddedItemCollectionFullEvents.Clear();
        }

        public override void AddEvent(plyEvent e)
        {
            if (e.uniqueIdent.Equals("Collection OnAddedItem"))
                onAddedItemsEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnUsedReference"))
                onUsedReferenceEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnUsedItem"))
                onUsedItemEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnUnstackedItemCollectionFull"))
                onUnstackedItemCollectionFullEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnUnstackedItem"))
                onUnstackedItemEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnSwappedItems"))
                onSwappedItemsEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnSorted"))
                onSortedEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnResized"))
                onResizedEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnRemovedReference"))
                onRemovedReferenceEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnRemovedItem"))
                onRemovedItemEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnDroppedItem"))
                onDroppedItemEvents.Add(e);

            if (e.uniqueIdent.Equals("Collection OnAddedItemCollectionFull"))
                onAddedItemCollectionFullEvents.Add(e);
        }

        public override void CheckEvents()
        {
            enabled = onAddedItemsEvents.Count > 0
                    || onUsedReferenceEvents.Count > 0
                    || onUsedItemEvents.Count > 0
                    || onUnstackedItemCollectionFullEvents.Count > 0
                    || onUnstackedItemEvents.Count > 0
                    || onSwappedItemsEvents.Count > 0
                    || onSortedEvents.Count > 0
                    || onResizedEvents.Count > 0
                    || onRemovedReferenceEvents.Count > 0
                    || onRemovedItemEvents.Count > 0
                    || onDroppedItemEvents.Count > 0
                    || onAddedItemCollectionFullEvents.Count > 0;
        }


        public void CollectionOnAddedItem(IEnumerable<InventoryItemBase> items, uint amount, bool cameFromCollection)
        {
            if (onAddedItemsEvents.Count > 0)
            {
                RunEvents(onAddedItemsEvents, 
                    new plyEventArg("items", items),
                    new plyEventArg("item", items.FirstOrDefault()),
                    new plyEventArg("itemID", (int)items.First().ID),
                    new plyEventArg("amount", (int)amount),
                    new plyEventArg("isLoot", cameFromCollection));
            }
        }

        public void CollectionOnUsedReference(InventoryItemBase actualItem, uint itemID, uint referenceSlot, uint amountUsed)
        {
            if (onUsedReferenceEvents.Count > 0)
            {
                RunEvents(onUsedReferenceEvents,
                    new plyEventArg("item", actualItem),
                    new plyEventArg("itemID", (int)itemID),
                    new plyEventArg("referenceSlot", (int)referenceSlot),
                    new plyEventArg("amount", (int)amountUsed));
            }
        }

        public void CollectionOnUsedItem(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {
            if (onUsedItemEvents.Count > 0)
            {
                RunEvents(onUsedItemEvents,
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)itemID),
                    new plyEventArg("slot", (int)slot),
                    new plyEventArg("amount", (int)amount));
            }
        }

        public void CollectionOnUnstackedItemCollectionFull(uint slot)
        {
            if (onUnstackedItemCollectionFullEvents.Count > 0)
            {
                RunEvents(onUnstackedItemCollectionFullEvents,
                    new plyEventArg("slot", (int)slot));
            }
        }

        public void CollectionOnUnstackedItem(uint fromSlot, uint toSlot, uint amount)
        {
            if (onUnstackedItemEvents.Count > 0)
            {
                RunEvents(onUnstackedItemEvents,
                    new plyEventArg("fromSlot", (int)fromSlot),
                    new plyEventArg("toSlot", (int)toSlot),
                    new plyEventArg("amount", (int)amount));
            }
        }

        public void CollectionOnSwappedItems(ItemCollectionBase fromCollection, uint fromSlot, ItemCollectionBase toCollection, uint toSlot)
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

        public void CollectionOnSorted()
        {
            if (onSortedEvents.Count > 0)
            {
                RunEvents(onSortedEvents);
            }
        }

        public void CollectionOnResized(uint fromSize, uint toSize)
        {
            if (onResizedEvents.Count > 0)
            {
                RunEvents(onResizedEvents,
                    new plyEventArg("fromSize", (int)fromSize),
                    new plyEventArg("toSize", (int)toSize));
            }
        }

        public void CollectionOnRemovedReference(InventoryItemBase item, uint slot)
        {
            if (onRemovedReferenceEvents.Count > 0)
            {
                RunEvents(onRemovedReferenceEvents,
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)item.ID),
                    new plyEventArg("slot", (int)slot));
            }
        }

        public void CollectionOnRemovedItem(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {
            if (onRemovedItemEvents.Count > 0)
            {
                RunEvents(onRemovedItemEvents,
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)itemID),
                    new plyEventArg("slot", (int)slot),
                    new plyEventArg("amount", (int)amount));
            }
        }

        public void CollectionOnDroppedItem(InventoryItemBase item, uint slot, UnityEngine.GameObject droppedObj)
        {
            if (onDroppedItemEvents.Count > 0)
            {
                RunEvents(onDroppedItemEvents,
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)item.ID),
                    new plyEventArg("slot", (int)slot),
                    new plyEventArg("droppedObject", droppedObj));
            }
        }

        public void CollectionOnAddedItemCollectionFull(InventoryItemBase item, bool cameFromCollection)
        {
            if (onAddedItemCollectionFullEvents.Count > 0)
            {
                RunEvents(onAddedItemCollectionFullEvents,
                    new plyEventArg("item", item),
                    new plyEventArg("itemID", (int)item.ID),
                    new plyEventArg("isLoot", cameFromCollection));
            }
        }
    }
}

#endif