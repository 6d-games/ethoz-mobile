#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get a stats value and store it in a FSM variable.")]
    public class GetStat : FsmStateAction
    {
        public InventoryPlayer player;

        public FsmString statCategoryName = "Default";
        public FsmString statName;

        [UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The result (final value) after adding")]
        public FsmFloat result;


        public bool everyFrame;


        public override void Reset()
        {

        }

        public override void OnUpdate()
        {
            DoGetStat();

            if (!everyFrame)
                Finish();
        }

        public override void OnEnter()
        {
            DoGetStat();
        }

        private void DoGetStat()
        {
            if (player == null)
            {
                player = PlayerManager.instance.currentPlayer.inventoryPlayer;
            }

            var stat = player.stats.Get(statCategoryName.Value, statName.Value);
            if (stat == null)
            {
                LogWarning("Stat in category " + statCategoryName + " with name " + statName + " does not exist.");
                return;
            }

            result.Value = stat.currentValue;
        }
    }
}

#endif