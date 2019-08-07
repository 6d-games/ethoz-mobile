#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Collections", "Can add item to collection", BlockType.Condition, ReturnValueType = typeof(Bool_Value), ReturnValueString = "Return - Boolean")]
    public class CanAddItemToCollection : Bool_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item you wist to add to the collection")]
        public InventoryItemBaseFieldData item;

        [plyBlockField("Amount", ShowName = true, ShowValue = true, DefaultObject = typeof(Int_Value), SubName = "Item amount", Description = "The amount of items you wish to add to the collection")]
        public Int_Value amount;

        [plyBlockField("Collection", ShowName = true, ShowValue = true, DefaultObject = typeof(ItemCollectionBase), EmptyValueName = "-error-", SubName = "InventorySystem collection", Description = "The collection you wish to add the item to")]
        public ItemCollectionBase collection;


        public override void Created()
        {
            blockIsValid = (item != null && amount.value > 0 && collection != null);

            if (amount.value <= 0)
                Log(LogType.Error, "Amount has to be higher than 0");

            if (collection == null)
                Log(LogType.Error, "Item, and collection have to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            item.item.currentStackSize = (uint)amount.value;
            value = collection.CanAddItem(item.item);
            item.item.currentStackSize = 1; // Reset
            return value ? BlockReturn.True : BlockReturn.False;
        }
    }
}

#endif