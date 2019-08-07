#if PLY_GAME

using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Items", "Open/Close treasure chest", BlockType.Action, Description = "Open or close a treasure chest.")]
    public class OpenCloseTreasureChest : plyBlock
    {
        [plyBlockField("Treasure chest", ShowName = true, ShowValue = true, DefaultObject = typeof(LootableObject), EmptyValueName = "-error-", SubName = "Treasure chest", Description = "The chest you wish to open / close")]
        public LootableObject chest;

        [plyBlockField("Chest action", ShowName = true, ShowValue = true, DefaultObject = typeof(Bool_Value), SubName = "Action", Description = "Open or close the chest?")]
        public Bool_Value action;

        public override void Created()
        {
            blockIsValid = chest != null;
        }

        public override BlockReturn Run(BlockReturn param)
        {
            if (action.value)
                chest.trigger.Use();
            else
                chest.trigger.UnUse();

            return BlockReturn.OK;
        }
    }
}

#endif