#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Collections", "Remove item from inventory", BlockType.Action, Description = "This removes the item from the inventory / inventories")]
    public class RemoveItemFromInventory : plyBlock
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item you wist to add to the collection")]
        public InventoryItemBaseFieldData item;

        [plyBlockField("Amount", ShowName = true, ShowValue = true, DefaultObject = typeof(Int_Value), SubName = "Item amount", Description = "The amount of items you wish to add to the collection")]
        public Int_Value amount;

        [plyBlockField("Check bank", ShowName = true, ShowValue = true, DefaultObject = typeof(Bool_Value), SubName = "Check bank", Description = "Allow the item to be removed from the bank as well, if none are found in the inventories.")]
        public Bool_Value checkBank;

        public override void Created()
        {
            blockIsValid = amount.value > 0 && item != null;

            if (amount.value <= 0)
                Log(LogType.Error, "Amount has to be higher than 0");

            if(item == null)
                Log(LogType.Error, "Item cannot be empty");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            InventoryManager.RemoveItem(item.item.ID, (uint)amount.value, checkBank.value);
            return BlockReturn.OK;
        }
    }
}

#endif