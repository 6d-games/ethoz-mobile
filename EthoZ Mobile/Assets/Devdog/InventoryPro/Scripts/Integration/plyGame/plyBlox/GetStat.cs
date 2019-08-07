#if PLY_GAME

using Devdog.General;
using plyBloxKit;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Stats", "Get stat", BlockType.Variable, Description = "Get the current player's stat.")]
    public class GetStat : Float_Value
    {
        [plyBlockField("Category name", ShowName = true, ShowValue = true, DefaultObject = typeof(String_Value), EmptyValueName = "-error-", SubName = "Stat category name", Description = "The category name of the stat.")]
        public String_Value categoryName;

        [plyBlockField("Stat name", ShowName = true, ShowValue = true, DefaultObject = typeof(String_Value), EmptyValueName = "-error-", SubName = "Stat name", Description = "The stat name.")]
        public String_Value statName;


        public override void Created()
        {

        }

        public override BlockReturn Run(BlockReturn param)
        {
            var stat = PlayerManager.instance.currentPlayer.inventoryPlayer.stats.Get(categoryName.value, statName.value);
            value = stat.currentValue;

            return BlockReturn.OK;
        }
    }
}

#endif