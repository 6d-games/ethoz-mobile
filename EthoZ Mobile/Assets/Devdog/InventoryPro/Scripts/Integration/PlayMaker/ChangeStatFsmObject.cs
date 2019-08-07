#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Change a stat fsm object.")]
    public class ChangeStatFsmObject : FsmStateAction
    {
        public enum ChangeType
        {
            MaxValue,
            MaxFactor,
            Factor,
            CurrentValue
        }

        public FsmObject player;

        public FsmFloat changeByAmount;
        [HutongGames.PlayMaker.Tooltip("When the maximum amount is increased should the current amount also incrase?\nFor example when you have 100 health and increase the max by 10 you'll have a max health of 110.\nWhen increaseCurrent is false health will stay at 100.\nWhen increaseCurrent is true healthw ill increase to 110.")]
        public bool increaseCurrentWhenIncreasingMax = true;

        public FsmString statCategoryName = "Default";
        public FsmString statName;

        public ChangeType changeType = ChangeType.CurrentValue;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The result (final value) after adding")]
        public FsmFloat result;

        public bool everyFrame;


        public override void Reset()
        {

        }


        public override void OnUpdate()
        {
            DoChangeStat();
        }

        public override void OnEnter()
        {
            DoChangeStat();

            if (!everyFrame)
                Finish();
        }

        private void DoChangeStat()
        {
            if (player.Value == null)
            {
                player.Value = PlayerManager.instance.currentPlayer.inventoryPlayer;
            }

            var p = player.Value as InventoryPlayer;
            if (p == null)
            {
                Finish();
                return;
            }

            var stat = p.stats.Get(statCategoryName.Value, statName.Value);
            if (stat == null)
            {
                LogWarning("Stat in category " + statCategoryName + " with name " + statName + " does not exist.");
                return;
            }

            switch (changeType)
            {
                case ChangeType.MaxValue:
                    stat.ChangeMaxValueRaw(changeByAmount.Value, increaseCurrentWhenIncreasingMax);
                    break;
                case ChangeType.MaxFactor:
                    stat.ChangeFactorMax(changeByAmount.Value, increaseCurrentWhenIncreasingMax);
                    break;
                case ChangeType.Factor:
                    stat.ChangeFactor(changeByAmount.Value);
                    break;
                case ChangeType.CurrentValue:
                    stat.ChangeCurrentValueRaw(changeByAmount.Value);
                    break;
                default:
                    break;
            }

            result.Value = stat.currentValue;
        }
    }
}

#endif