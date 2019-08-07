#if PLY_GAME

using Devdog.General;
using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Stats", "Set stat", BlockType.Action, Description = "Set a stat on the current player.")]
    public class SetStat : plyBlock
    {
        public enum ChangeType
        {
            MaxValue,
            MaxFactor,
            Factor,
            CurrentValue
        }


        [plyBlockField("Category name", ShowName = true, ShowValue = true, DefaultObject = typeof(String_Value), EmptyValueName = "-error-", SubName = "Stat category name", Description = "The category name of the stat.")]
        public String_Value categoryName;

        [plyBlockField("Stat name", ShowName = true, ShowValue = true, DefaultObject = typeof(String_Value), EmptyValueName = "-error-", SubName = "Stat name", Description = "The stat name.")]
        public String_Value statName;


        [plyBlockField("Set to value", ShowName = true, ShowValue = true, DefaultObject = typeof(Float_Value), EmptyValueName = "-error-", SubName = "Set to value", Description = "The value the stat is supposed to get.")]
        public Float_Value newValue;



        [plyBlockField("Increase current when increasing max", ShowName = true, ShowValue = true, DefaultObject = typeof(bool), EmptyValueName = "-error-", SubName = "Change current when increasing max", Description = "When the maximum value is increased, should the current value also increase?.")]
        public bool increaseCurrentWhenIncreasingMax = false;

        [plyBlockField("Increase type", ShowName = true, ShowValue = true, DefaultObject = typeof(ChangeType), EmptyValueName = "-error-", SubName = "Increase type", Description = "What part of the stat should be increased?")]
        public ChangeType changeType = ChangeType.CurrentValue;


        public override void Created()
        {

        }

        public override BlockReturn Run(BlockReturn param)
        {
            var player = PlayerManager.instance.currentPlayer.inventoryPlayer;
            if (player.characterUI == null)
            {
                Log(LogType.Warning, "No character collection set on player.");
                return BlockReturn.Error;
            }

            var stat = player.stats.Get(categoryName.value, statName.value);
            if (stat == null)
            {
                Log(LogType.Warning, "Stat in category " + categoryName.value + " with name " + statName.value + " does not exist.");
                return BlockReturn.Error;
            }

            switch (changeType)
            {
                case ChangeType.MaxValue:
                    stat.SetMaxValueRaw(newValue.value, increaseCurrentWhenIncreasingMax);
                    break;
                case ChangeType.MaxFactor:
                    stat.SetFactorMax(newValue.value, increaseCurrentWhenIncreasingMax);
                    break;
                case ChangeType.Factor:
                    stat.SetFactor(newValue.value);
                    break;
                case ChangeType.CurrentValue:
                    stat.SetCurrentValueRaw(newValue.value);
                    break;
                default:
                    break;
            }

            return BlockReturn.OK;
        }
    }
}

#endif