#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items", "Get item from slot", BlockType.Variable, Description = "Get the item from a wrapper.")]
    public class GetItemFromSlot : SystemObject_Value
    {
        [plyBlockField("Slot", ShowName = true, ShowValue = true, DefaultObject = typeof(ItemCollectionSlotUI), EmptyValueName = "-error-", Description = "The Slot to get the item from.")]
        public ItemCollectionSlotUI slot;

        public override void Created()
        {

        }

        public override BlockReturn Run(BlockReturn param)
        {
            value = slot.item;

            return BlockReturn.OK;
        }
    }
}

#endif