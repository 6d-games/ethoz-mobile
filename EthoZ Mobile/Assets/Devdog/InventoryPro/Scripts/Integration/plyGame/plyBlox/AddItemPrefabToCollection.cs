#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Collections", "Add item prefab to collection", BlockType.Action)]
    public class AddItemPrefabToCollection : plyBlock
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

            if(blockIsValid == false)
                Log(LogType.Error, "Item, and collection have to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            var i = UnityEngine.Object.Instantiate<InventoryItemBase>(item.item); // Make copy
            i.currentStackSize = (uint)amount.value;

            bool added = collection.AddItem(i);
            if (added)
                return BlockReturn.OK;

            return BlockReturn.Continue;
        }
    }
}

#endif