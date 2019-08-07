#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Collections", "Add item to inventory", BlockType.Action, Description = "This adds the item to the \"best\" inventory for the item type and amount. Based on onlyAllowItemsOfType and priority.")]
    public class AddItemToInventory : plyBlock
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBase), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item you wist to add to the collection")]
        public InventoryItemBase item;

        [plyBlockField("Amount", ShowName = true, ShowValue = true, DefaultObject = typeof(Int_Value), SubName = "Item amount", Description = "The amount of items you wish to add to the collection")]
        public Int_Value amount;

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
            var i = UnityEngine.Object.Instantiate<InventoryItemBase>(item); // Make copy
            i.currentStackSize = (uint)amount.value;

            bool added = InventoryManager.AddItem(i);
            if (added)
                return BlockReturn.OK;

            return BlockReturn.Continue;
        }
    }
}

#endif