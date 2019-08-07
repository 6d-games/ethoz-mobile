#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items", "Use item", BlockType.Action, ReturnValueType = typeof(UseItem))]
    public class UseItem : Bool_Value
    {
        [plyBlockField("Item", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBase), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "Item to use, make sure it's a scene object, and not a prefab.")]
        public InventoryItemBase item;

        public override void Created()
        {
            blockIsValid = item != null;
        }

        public override BlockReturn Run(BlockReturn param)
        {
            item.Use();
            return BlockReturn.OK;
        }
    }
}

#endif