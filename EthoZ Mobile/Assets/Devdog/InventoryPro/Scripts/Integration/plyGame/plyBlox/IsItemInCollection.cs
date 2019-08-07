#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items", "Is item in collection", BlockType.Condition, ReturnValueType = typeof(Bool_Value), ReturnValueString = "Return - Boolean")]
    public class IsItemInCollection : Bool_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBase), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item you wist to add to the collection")]
        public InventoryItemBase item;


        public override void Created()
        {
            blockIsValid = (item != null);

            if(blockIsValid == false)
                Log(LogType.Error, "Item has to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            return item.itemCollection != null ? BlockReturn.True : BlockReturn.False;
        }
    }
}

#endif