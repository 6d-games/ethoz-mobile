#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Collections", "Add item prefab to inventory", BlockType.Action, Description = "This adds the item to the \"best\" inventory for the item type and amount. Based on onlyAllowItemsOfType and priority.")]
    public class AddItemPrefabToInventory : plyBlock
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item you wist to add to the collection")]
        public InventoryItemBaseFieldData item;

        [plyBlockField("Amount", ShowName = true, ShowValue = true, DefaultObject = typeof(Int_Value), SubName = "Item amount", Description = "The amount of items you wish to add to the collection")]
        public Int_Value amount;

        public override void Created()
        {
            blockIsValid = (item != null && amount.value > 0);

            if (amount.value <= 0)
                Log(LogType.Error, "Amount has to be higher than 0");

            if(item == null)
                Log(LogType.Error, "Item cannot be empty");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            var i = UnityEngine.Object.Instantiate<InventoryItemBase>(item.item); // Make copy
            i.currentStackSize = (uint)amount.value;

            bool added = InventoryManager.AddItem(i);
            if (added)
                return BlockReturn.OK;

            return BlockReturn.Continue;
        }
    }
}

#endif