#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items - Instance", "Get item ID instance", BlockType.Variable, Order = 4, ShowName = "Get item ID instance",
        ReturnValueString = "Return - Integer (ID)", ReturnValueType = typeof(Int_Value),
        CustomStyle = "plyBlox_VarYellowDark", Description = "Returns the given item's ID of an instance object.")]
    public class GetItemIDInstance : Int_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(SystemObject_Value), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The instance item (System.Object).")]
        public SystemObject_Value item;

        public override void Created()
        {
            blockIsValid = item.value is InventoryItemBase;
            
            if(blockIsValid == false)
                Log(LogType.Error, "Given item is empty or not an InventoryItemBase item.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            var i = item.value as InventoryItemBase;
            if (i == null)
            {
                return BlockReturn.Error;
            }

            value = (int)i.ID;
            return BlockReturn.OK;
        }
    }
}

#endif