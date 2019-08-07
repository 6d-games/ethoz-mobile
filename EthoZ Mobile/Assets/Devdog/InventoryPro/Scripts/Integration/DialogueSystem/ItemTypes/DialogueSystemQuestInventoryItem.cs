#if DIALOGUE_SYSTEM

using System.Collections.Generic;
using Devdog.General;
using Devdog.General.ThirdParty.UniLinq;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Devdog.InventoryPro
{
    /// <summary>
    /// Used to represent items that can be used by the player, and once used reduce 1 in stack size. This includes potions, food, scrolls, etc.
    /// </summary>
    public partial class DialogueSystemQuestInventoryItem : InventoryItemBase
    {
        /// <summary>
        /// When the item is used, play this sound.
        /// </summary>
        public General.AudioClipInfo audioClipWhenUsed;

        public string acceptMessage = "Accepted {0}";
        public string questAlreadyActiveMessage = "{0} is already active";

        public bool canUseAfterQuestAlreadyCompleted = false;
        public string questAlreadyUsedMessage = "{0} is already done";

        [SerializeField]
        private int _dialogueSystemQuestID;

        

        public override LinkedList<ItemInfoRow[]> GetInfo()
        {
            var basic = base.GetInfo();
            //basic.AddAfter(basic.First, new InfoBoxUI.Row[]{
            //    new InfoBoxUI.Row("Restore health", restoreHealth.ToString(), Color.green, Color.green),
            //    new InfoBoxUI.Row("Restore mana", restoreMana.ToString(), Color.green, Color.green)
            //});


            return basic;
        }

        public override void NotifyItemUsed(uint amount, bool alsoNotifyCollection)
        {
            base.NotifyItemUsed(amount, alsoNotifyCollection);

            PlayerManager.instance.currentPlayer.inventoryPlayer.stats.SetAll(stats);
        }

        public override int Use()
        {
            int used = base.Use();
            if(used < 0)
                return used;

            var dialogueController = FindObjectOfType<DialogueSystemController>();
            if (dialogueController == null)
            {
                Debug.LogError("Couldn't find DialogueSystemController; Can't activate quest!");
                return -2;
            }
            
            var item = dialogueController.DatabaseManager.MasterDatabase.items.FirstOrDefault(o => o.id == _dialogueSystemQuestID);
            if (item == null)
            {
                Debug.LogError("Couldn't find dialogue quest with ID " + _dialogueSystemQuestID);
                return -2;
            }

            if (item.IsItem)
            {
                Debug.LogError("Dialogue quest with ID " + _dialogueSystemQuestID + " is an item, not a quest");
                return -2;
            }

            if (QuestLog.IsQuestActive(item.Name))
            {
                DialogueManager.ShowAlert(string.Format(questAlreadyActiveMessage, item.Name));
                return -2;
            }

            if (QuestLog.IsQuestDone(item.Name) || QuestLog.IsQuestFailed(item.Name) || QuestLog.IsQuestAbandoned(item.Name))
            {
                if (canUseAfterQuestAlreadyCompleted == false)
                {
                    DialogueManager.ShowAlert(string.Format(questAlreadyUsedMessage, item.Name));
                    return -2;
                }
            }


            currentStackSize--; // Remove 1

            QuestLog.StartQuest(item.Name);
            QuestLog.AddQuestStateObserver(item.Name, LuaWatchFrequency.EveryDialogueEntry, QuestChangedHandler);

            if (string.IsNullOrEmpty(acceptMessage) == false)
            {
                DialogueManager.ShowAlert(string.Format(acceptMessage, item.Name));
            }


            NotifyItemUsed(1, true);
            AudioManager.AudioPlayOneShot(audioClipWhenUsed);

            return 1; // 1 item used
        }

        private void QuestChangedHandler(string title, QuestState newState)
        {
            if (newState == QuestState.Success)
            {
                Debug.Log("Quest completed callback");       
            }
        }
    }
}

#endif