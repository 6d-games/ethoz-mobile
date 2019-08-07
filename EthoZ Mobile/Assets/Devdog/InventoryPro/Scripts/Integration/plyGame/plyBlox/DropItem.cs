#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items", "Drop item", BlockType.Action, Description = "Drop the item, this will trigger the UI, if you wish to drop without any UI either disable in the settings, or use DropItemNow")]
    public class DropItem : plyBlock
    {
        [plyBlockField("Item to drop", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBase), EmptyValueName = "-this-", SubName = "InventorySystem item", Description = "The item you wist to drop")]
        public InventoryItemBase item;

        public override void Created()
        {
            blockIsValid = item != null;
            
        }

        public override BlockReturn Run(BlockReturn param)
        {
            item.itemCollection[item.index].TriggerDrop();
            return BlockReturn.OK;
        }
    }
}

#endif