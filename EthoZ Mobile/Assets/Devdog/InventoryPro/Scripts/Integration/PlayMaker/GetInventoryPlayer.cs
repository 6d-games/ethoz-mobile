#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the currently active inventory player.")]
    public class GetInventoryPlayer : FsmStateAction
    {
        public FsmObject player;
        public bool everyFrame;

        public override void Reset()
        {

        }

        public override void OnUpdate()
        {
            DoGetPlayer();

            if (!everyFrame)
                Finish();
        }

        public override void OnEnter()
        {
            DoGetPlayer();
        }

        private void DoGetPlayer()
        {
            player = PlayerManager.instance.currentPlayer.inventoryPlayer;
		}
    }
}

#endif