#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items", "Get item type name", BlockType.Variable, Order = 4, ShowName = "Get item type name",
        ReturnValueString = "Return - String (Name)", ReturnValueType = typeof(String_Value),
        CustomStyle = "plyBlox_VarYellowDark", Description = "Returns the given item's type name.")]
    public class GetItemTypeName : String_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item we wish to get the type name of.")]
        public InventoryItemBaseFieldData item;

        public override void Created()
        {
            blockIsValid = (item != null);
            
            if(blockIsValid == false)
                Log(LogType.Error, "Item has to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            value = item.item.GetType().ToString();
            return BlockReturn.OK;
        }
    }
}

#endif