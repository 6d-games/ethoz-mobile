#if PLY_GAME

using Devdog.General;
using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Stats", "Change stat", BlockType.Action, Description = "Change a stat on the current player.")]
    public class ChangeStat : plyBlock
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


        [plyBlockField("Change by value", ShowName = true, ShowValue = true, DefaultObject = typeof(Float_Value), EmptyValueName = "-error-", SubName = "Change by value", Description = "Change stat by this much.")]
        public Float_Value changeByValue;


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
            var stat = player.stats.Get(categoryName.value, statName.value);
            if (stat == null)
            {
                Log(LogType.Warning, "Stat in category " + categoryName.value + " with name " + statName.value + " does not exist.");
                return BlockReturn.Error;
            }

            switch (changeType)
            {
                case ChangeType.MaxValue:
                    stat.ChangeMaxValueRaw(changeByValue.value, increaseCurrentWhenIncreasingMax);
                    break;
                case ChangeType.MaxFactor:
                    stat.ChangeFactorMax(changeByValue.value, increaseCurrentWhenIncreasingMax);
                    break;
                case ChangeType.Factor:
                    stat.ChangeFactor(changeByValue.value);
                    break;
                case ChangeType.CurrentValue:
                    stat.ChangeCurrentValueRaw(changeByValue.value);
                    break;
                default:
                    break;
            }

            return BlockReturn.OK;
        }
    }
}

#endif