#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items", "Get item category name", BlockType.Variable, Order = 4, ShowName = "Get item category name",
        ReturnValueString = "Return - String (Name)", ReturnValueType = typeof(String_Value),
        CustomStyle = "plyBlox_VarYellowDark", Description = "Returns the given item's category name.")]
    public class GetItemCategoryName : String_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item we wish to get the category name of.")]
        public InventoryItemBaseFieldData item;

        public override void Created()
        {
            blockIsValid = (item != null);
            
            if(blockIsValid == false)
                Log(LogType.Error, "Item has to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            value = item.item.category.name;
            return BlockReturn.OK;
        }
    }
}

#endif