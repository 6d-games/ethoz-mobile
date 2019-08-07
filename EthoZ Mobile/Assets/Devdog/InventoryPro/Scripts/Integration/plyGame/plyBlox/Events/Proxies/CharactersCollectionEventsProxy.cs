#if PLY_GAME

using System.Collections.Generic;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{

    /// <summary>
    /// Relays all events to plyGame's plyBox
    /// </summary>
    public partial class CharactersCollectionEventsProxy : MonoBehaviour
    {
        private CharactersEventHandler eventHandler { get; set; }
        private InventoryPlayer player { get; set; }
        
        // <inheritdoc />
        public void Start()
        {
            player = GetComponent<InventoryPlayer>();
            eventHandler = GetComponent<CharactersEventHandler>();

            if (player.characterUI != null)
            {
                player.characterUI.OnAddedItem += OnAddedItemPly;
                player.characterUI.OnRemovedItem += OnRemovedItemPly;
                player.characterUI.OnSwappedItems += OnSwappedItemsPly;
            }
        }


        public void OnDestroy()
        {
            if (player.characterUI != null)
            {
                player.characterUI.OnAddedItem -= OnAddedItemPly;
                player.characterUI.OnRemovedItem -= OnRemovedItemPly;
                player.characterUI.OnSwappedItems -= OnSwappedItemsPly;
            }
        }

        private void OnAddedItemPly(IEnumerable<InventoryItemBase> items, uint amount, bool cameFromCollection)
        {
            if (eventHandler != null)
                eventHandler.CharacterOnEquippedItem(items, amount, cameFromCollection);
        }

        private void OnSwappedItemsPly(ItemCollectionBase fromCollection, uint fromSlot, ItemCollectionBase toCollection, uint toSlot)
        {
            if (eventHandler != null)
                eventHandler.CharacterOnSwappedItems(fromCollection, fromSlot, toCollection, toSlot);
        }

        private void OnRemovedItemPly(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {
            if (eventHandler != null)
                eventHandler.CharacterOnUnEquippedItem(item, itemID, slot, amount);
        }
    }
}

#endif