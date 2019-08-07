#if REWIRED

using Devdog.General;
using Rewired;

namespace Devdog.InventoryPro.Integration.RewiredIntegration
{
    public class RewiredItemTriggerInputHandler : ItemTriggerInputHandler
    {
        public int playerID = 0;
        public string rewiredActionName = "PickupItem";

        private Rewired.Player _player;
        protected override void Awake()
        {
            base.Awake();
            _player = ReInput.players.GetPlayer(playerID);
        }

        public override TriggerActionInfo actionInfo
        {
            get
            {
                return new TriggerActionInfo()
                {
                    actionName = rewiredActionName,
                    icon = uiIcon
                };
            }
        }

        public override bool AreKeysDown()
        {
            return _player.GetButtonDown(rewiredActionName);
        }

        public override string ToString()
        {
            return rewiredActionName;
        }
    }
}

#endif