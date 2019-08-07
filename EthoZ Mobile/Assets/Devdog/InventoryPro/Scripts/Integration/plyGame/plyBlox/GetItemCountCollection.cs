#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Collections", "Get item count collection", BlockType.Variable, Order = 4, ShowName = "Get item count collection",
        ReturnValueString = "Return - Integer (amount)", ReturnValueType = typeof(Int_Value),
        CustomStyle = "plyBlox_VarYellowDark", Description = "Returns the item count in the given collection.")]
    public class GetItemCountCollection : Int_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBaseFieldData), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "The item of which you can to check the amount.")]
        public InventoryItemBaseFieldData item;

        [plyBlockField("Collection", ShowName = true, ShowValue = true, DefaultObject = typeof(ItemCollectionBase), EmptyValueName = "-error-", SubName = "InventorySystem collection", Description = "The collection you wish to check.")]
        public ItemCollectionBase collection;


        public override void Created()
        {
            blockIsValid = (item != null && collection != null);
            
            if(blockIsValid == false)
                Log(LogType.Error, "Item, and collection have to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            value = (int)collection.GetItemCount(item.item.ID);
            return BlockReturn.OK;
        }
    }
}

#endif