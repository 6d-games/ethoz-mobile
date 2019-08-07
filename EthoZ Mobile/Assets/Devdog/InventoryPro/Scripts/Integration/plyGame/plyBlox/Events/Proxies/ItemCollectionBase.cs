#if PLY_GAME

using System.Collections.Generic;
using Devdog.InventoryPro.Integration.plyGame.plyBlox;
using UnityEngine;

namespace Devdog.InventoryPro
{

    /// <summary>
    /// Relays all events to plyGame's plyBox
    /// </summary>
    public partial class ItemCollectionBase
    {
        private CollectionEventHandler eventHandler { get; set; }
        
        
        // <inheritdoc />
        partial void Start2()
        {
            var c = GetComponent<ItemCollectionBase>();
            eventHandler = gameObject.GetComponent<CollectionEventHandler>();

            c.OnAddedItem += OnAddedItemPly;
//            c.OnAddedItemCollectionFull += OnAddedItemCollectionFullPly;
            c.OnDroppedItem += OnDroppedItemPly;
            c.OnRemovedItem += OnRemovedItemPly;
            c.OnRemovedReference += OnRemovedReferencePly;
            c.OnResized += OnResizedPly;
            c.OnSorted += OnSortedPly;
            c.OnSwappedItems += OnSwappedItemsPly;
            c.OnUnstackedItem += OnUnstackedItemPly;
            c.OnUsedItem += OnUsedItemPly;
            c.OnUsedReference += OnUsedReferencePly;
        }


        partial void OnDestroy2()
        {
            var c = GetComponent<ItemCollectionBase>();

            c.OnAddedItem -= OnAddedItemPly;
//            c.OnAddedItemCollectionFull -= OnAddedItemCollectionFullPly;
            c.OnDroppedItem -= OnDroppedItemPly;
            c.OnRemovedItem -= OnRemovedItemPly;
            c.OnRemovedReference -= OnRemovedReferencePly;
            c.OnResized -= OnResizedPly;
            c.OnSorted -= OnSortedPly;
            c.OnSwappedItems -= OnSwappedItemsPly;
            c.OnUnstackedItem -= OnUnstackedItemPly;
            c.OnUsedItem -= OnUsedItemPly;
            c.OnUsedReference -= OnUsedReferencePly;
        }

        private void OnAddedItemPly(IEnumerable<InventoryItemBase> items, uint amount, bool cameFromCollection)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnAddedItem(items, amount, cameFromCollection);
        }

        private void OnUsedReferencePly(InventoryItemBase actualItem, uint itemID, uint referenceSlot, uint amountUsed)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnUsedReference(actualItem, itemID, referenceSlot, amountUsed);
        }

        private void OnUsedItemPly(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnUsedItem(item, itemID, slot, amount);
        }

        private void OnUnstackedItemPly(ItemCollectionBase fromCollection, uint startSlot, ItemCollectionBase toCollection, uint endSlot, uint amount)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnUnstackedItem(startSlot, endSlot, amount);
        }

        private void OnSwappedItemsPly(ItemCollectionBase fromCollection, uint fromSlot, ItemCollectionBase toCollection, uint toSlot)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnSwappedItems(fromCollection, fromSlot, toCollection, toSlot);
        }

        private void OnSortedPly()
        {
            if (eventHandler != null)
                eventHandler.CollectionOnSorted();
        }

        private void OnResizedPly(uint fromSize, uint toSize)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnResized(fromSize, toSize);
        }

        private void OnRemovedReferencePly(InventoryItemBase item, uint slot)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnRemovedReference(item, slot);
        }

        private void OnRemovedItemPly(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnRemovedItem(item, itemID, slot, amount);
        }

        private void OnDroppedItemPly(InventoryItemBase item, uint slot, GameObject droppedObj)
        {
            if (eventHandler != null)
                eventHandler.CollectionOnDroppedItem(item, slot, droppedObj);
        }

//        private void OnAddedItemCollectionFullPly(InventoryItemBase item, bool cameFromCollection)
//        {
//            if (eventHandler != null)
//                eventHandler.CollectionOnAddedItemCollectionFull(item, cameFromCollection);
//        }
    }
}

#endif