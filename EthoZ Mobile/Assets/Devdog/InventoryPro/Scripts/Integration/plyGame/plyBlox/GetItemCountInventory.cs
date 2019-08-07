#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Collections", "Get item count inventories", BlockType.Variable, Order = 4, ShowName = "Get item count inventories",
        ReturnValueString = "Return - Integer (amount)", ReturnValueType = typeof(Int_Value),
        CustomStyle = "plyBlox_VarYellowDark", Description = "Returns the item count in all inventories.")]
    public class GetItemCountInventory : Int_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item of which you can to check the amount.")]
        public InventoryItemBaseFieldData item;

        [plyBlockField("Check bank", ShowName = true, ShowValue = true, DefaultObject = typeof(Bool_Value), Description = "Also count items in the bank.")]
        public Bool_Value checkBank;


        public override void Created()
        {
            blockIsValid = (item != null);
            
            if(blockIsValid == false)
                Log(LogType.Error, "Item, and collection have to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            value = (int)InventoryManager.GetItemCount(item.item.ID, checkBank.value);
            return BlockReturn.OK;
        }
    }
}

#endif