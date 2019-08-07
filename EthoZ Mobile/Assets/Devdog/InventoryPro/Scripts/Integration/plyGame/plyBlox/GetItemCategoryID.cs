#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items", "Get item category ID", BlockType.Variable, Order = 4, ShowName = "Get item category ID",
        ReturnValueString = "Return - String (Name)", ReturnValueType = typeof(Int_Value),
        CustomStyle = "plyBlox_VarYellowDark", Description = "Returns the given item's category ID.")]
    public class GetItemCategoryID : Int_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item we wish to get the category ID of.")]
        public InventoryItemBaseFieldData item;

        public override void Created()
        {
            blockIsValid = (item != null);
            
            if(blockIsValid == false)
                Log(LogType.Error, "Item has to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            value = (int)item.item.category.ID;
            return BlockReturn.OK;
        }
    }
}

#endif