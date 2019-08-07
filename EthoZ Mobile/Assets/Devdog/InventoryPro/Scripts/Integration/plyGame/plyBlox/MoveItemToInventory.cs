#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock(InventoryPro.ProductName, "Collections", "Move item to inventory", BlockType.Action)]
    public class MoveItemToInventory : plyBlock
    {
        [plyBlockField("Slot", ShowName = true, ShowValue = true, DefaultObject = typeof(ItemCollectionSlotUI), EmptyValueName = "-error-", SubName = "Slot", Description = "The slot that contains the item that you want to move to the inventory.")]
        public ItemCollectionSlotUI slot;
        

        public override void Created()
        {
            blockIsValid = (slot != null);
            
            if(blockIsValid == false)
                Log(LogType.Error, "Slot has to be set.");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            if (slot.item != null)
            {
                InventoryManager.AddItemAndRemove(slot.item);
            }

            return BlockReturn.OK;
        }
    }
}

#endif