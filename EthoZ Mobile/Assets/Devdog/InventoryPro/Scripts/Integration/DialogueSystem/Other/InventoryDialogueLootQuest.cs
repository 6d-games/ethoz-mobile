#if DIALOGUE_SYSTEM

using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.DialogueSystem
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/DialogueSystem/Inventory dialogue loot quest")]
    public class InventoryDialogueLootQuest : MonoBehaviour
    {
        /// <summary>
		/// The variable to increment.
		/// </summary>
		public string variable = string.Empty;
        public ItemAmountRow requiredItem = new ItemAmountRow();
        
        public string alertMessage = string.Empty;


        private ItemCollectionBase[] _collections = new ItemCollectionBase[0];

        private uint _currentCount = 0;

        protected string actualVariableName
        {
            get { return string.IsNullOrEmpty(variable) ? OverrideActorName.GetInternalName(transform) : variable; }
        }


        public void Start()
        {
            _collections = InventoryManager.GetLootToCollections();

            foreach (var col in _collections)
            {
                col.OnAddedItem += OnAddedItem;
                col.OnRemovedItem += OnRemoevdItem;
                col.OnUsedItem += OnUsedItem;
            }
        }

        public void OnDestroy()
        {
            foreach (var col in _collections)
            {
                if (col == null)
                {
                    continue;
                }

                col.OnAddedItem -= OnAddedItem;
                col.OnRemovedItem -= OnRemoevdItem;
                col.OnUsedItem -= OnUsedItem;
            }
        }



        private void OnUsedItem(InventoryItemBase item, uint itemid, uint slot, uint amount)
        {
            if(item.ID == requiredItem.item.ID)
                UpdateCount(item.ID);
        }

        private void OnRemoevdItem(InventoryItemBase item, uint itemid, uint slot, uint amount)
        {
            if(item.ID == requiredItem.item.ID)
                UpdateCount(item.ID);
        }
        
        private void OnAddedItem(IEnumerable<InventoryItemBase> items, uint amount, bool camefromcollection)
        {
            if(items.First().ID == requiredItem.item.ID)
                UpdateCount(items.First().ID);
        }

        private void UpdateCount(uint itemID)
        {
            _currentCount = InventoryManager.GetItemCount(itemID, false);

            DialogueLua.SetVariable(actualVariableName, (int)_currentCount);
            DialogueManager.SendUpdateTracker();
            
            if (!string.IsNullOrEmpty(alertMessage))
            {
                DialogueManager.ShowAlert(alertMessage);
            }
        }
    }
}

#endif