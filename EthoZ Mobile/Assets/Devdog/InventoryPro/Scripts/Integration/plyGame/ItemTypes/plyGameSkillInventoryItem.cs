#if PLY_GAME

using Devdog.General;
using plyGame;
using Player = plyGame.Player;
using PlayerManager = Devdog.General.PlayerManager;

namespace Devdog.InventoryPro.Integration.plyGame
{
    public partial class plyGameSkillInventoryItem : InventoryItemBase
    {

        [Required]
        public Skill skill;


        public override void NotifyItemUsed(uint amount, bool alsoNotifyCollection)
        {
            base.NotifyItemUsed(amount, alsoNotifyCollection);

            PlayerManager.instance.currentPlayer.inventoryPlayer.stats.SetAll(stats);
        }

        public override int Use()
        {
            int used = base.Use();
            if (used < 0)
                return used;

            if (Player.Instance.actor.actorClass.currLevel < requiredLevel)
            {
                InventoryManager.langDatabase.itemCannotBeUsedLevelToLow.Show(name, description, requiredLevel);
                return -1;
            }

            // Use plyGame skill
            if (skill != null)
            {
                Player.Instance.actor.QueueSkillForExecution(skill, true);
            }

            NotifyItemUsed(1, true);
            return 1;
        }
    }
}

#endif