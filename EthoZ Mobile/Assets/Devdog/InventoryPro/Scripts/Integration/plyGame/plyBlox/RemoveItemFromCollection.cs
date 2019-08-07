#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Collections", "Remove item from collection", BlockType.Action, Description = "This removes the item from the selected collection")]
    public class RemoveItemFromCollection : plyBlock
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item you wist to add to the collection")]
        public InventoryItemBaseFieldData item;

        [plyBlockField("Amount", ShowName = true, ShowValue = true, DefaultObject = typeof(Int_Value), SubName = "Item amount", Description = "The amount of items you wish to add to the collection")]
        public Int_Value amount;

        [plyBlockField("Collection", ShowName = true, ShowValue = true, DefaultObject = typeof(ItemCollectionBase), SubName = "Collection", Description = "The collection of which to remove the item")]
        public ItemCollectionBase collection;


        public override void Created()
        {
            blockIsValid = amount.value > 0 && item != null && collection != null;

            if (amount.value <= 0)
                Log(LogType.Error, "Amount has to be higher than 0");

            if(item == null)
                Log(LogType.Error, "Item cannot be empty");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            collection.RemoveItem(item.item.ID, (uint)amount.value);
            return BlockReturn.OK;
        }
    }
}

#endif