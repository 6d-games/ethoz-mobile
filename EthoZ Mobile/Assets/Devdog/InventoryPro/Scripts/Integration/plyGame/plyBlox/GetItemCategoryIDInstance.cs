#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items - Instance", "Get item category ID instance", BlockType.Variable, Order = 4, ShowName = "Get item category ID",
        ReturnValueString = "Return - String (Name)", ReturnValueType = typeof(Int_Value),
        CustomStyle = "plyBlox_VarYellowDark", Description = "Returns the given item's category ID of an instance object.")]
    public class GetItemCategoryIDInstance : Int_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(SystemObject_Value), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item instance we wish to get the category ID of.")]
        public SystemObject_Value item;

        public override void Created()
        {
            blockIsValid = item.value is InventoryItemBase;

            if (blockIsValid == false)
                Log(LogType.Error, "Item has to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            var i = item.value as InventoryItemBase;
            if (i == null)
            {
                return BlockReturn.Error;
            }

            value = (int)i.category.ID;
            return BlockReturn.OK;
        }
    }
}

#endif