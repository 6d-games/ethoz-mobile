#if DIALOGUE_SYSTEM

using Devdog.General.ThirdParty.UniLinq;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.DialogueSystem
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/DialogueSystem/Inventory manager dialogue system")]
    public class InventoryManagerDialogueSystem : MonoBehaviour
    {
        protected virtual void Start()
        {
            // Register the LUA actions:
            Lua.RegisterFunction("CanAddItem", null, SymbolExtensions.GetMethodInfo(() => CanAddItem(string.Empty, 0f)));
            Lua.RegisterFunction("AddItem", null, SymbolExtensions.GetMethodInfo(() => AddItem(string.Empty, 0f)));
            Lua.RegisterFunction("RemoveItem", null, SymbolExtensions.GetMethodInfo(() => RemoveItem(string.Empty, 0f)));
            Lua.RegisterFunction("GetItemCount", null, SymbolExtensions.GetMethodInfo(() => GetItemCount(string.Empty)));

            Lua.RegisterFunction("CanRemoveCurrency", null, SymbolExtensions.GetMethodInfo(() => CanRemoveCurrency(string.Empty, 0f)));
            Lua.RegisterFunction("AddCurrency", null, SymbolExtensions.GetMethodInfo(() => AddCurrency(string.Empty, 0f)));
            Lua.RegisterFunction("RemoveCurrency", null, SymbolExtensions.GetMethodInfo(() => RemoveCurrency(string.Empty, 0f)));
            Lua.RegisterFunction("GetCurrency", null, SymbolExtensions.GetMethodInfo(() => GetCurrencyCount(string.Empty)));

            Debug.Log("Inventory Pro - Registered Dialogue System LUA Connections.");
        }

        private static CurrencyDefinition GetCurrencyFromID(string currencyName)
        {
            int itemID;
            bool isItemID = int.TryParse(currencyName, out itemID);
            if (isItemID)
                return ItemManager.database.currencies.FirstOrDefault(o => o.ID == itemID);

            // Find by name
            return ItemManager.database.currencies.FirstOrDefault(o => o.singleName == currencyName || o.pluralName == currencyName);
        }

        private static InventoryItemBase GetItemFromID(string itemName)
        {
            int itemID;
            bool isItemID = int.TryParse(itemName, out itemID);
            if (isItemID)
                return ItemManager.database.items[itemID];

            // Find by name
            return ItemManager.database.items.FirstOrDefault(o => o.name == itemName);
        }

        private static bool CanAddItem(string itemID, float amount)
        {
            return InventoryManager.CanAddItem(new ItemAmountRow(GetItemFromID(itemID), (uint)amount));
        }
        
        private static void AddItem(string itemID, float amount)
        {
            InventoryItemBase item = GetItemFromID(itemID);

            item = Instantiate<InventoryItemBase>(item);
            item.currentStackSize = (uint)amount;

            InventoryManager.AddItem(item);
        }

        private static void RemoveItem(string itemID, float amount)
        {
            InventoryManager.RemoveItem(GetItemFromID(itemID).ID, (uint)amount, false);
        }

        private static float GetItemCount(string itemID)
        {
            return InventoryManager.GetItemCount(GetItemFromID(itemID).ID, false);
        }
        
        private static bool CanRemoveCurrency(string currencyID, float amount)
        {
            return InventoryManager.CanRemoveCurrency(GetCurrencyFromID(currencyID).ID, amount, true);
        }
        
        private static void AddCurrency(string currencyID, float amount)
        {
            InventoryManager.AddCurrency(GetCurrencyFromID(currencyID).ID, amount);
        }

        private static void RemoveCurrency(string currencyID, float amount)
        {
        InventoryManager.RemoveCurrency(GetCurrencyFromID(currencyID).ID, amount);
        }

        private static float GetCurrencyCount(string currencyID)
        {
            return InventoryManager.GetCurrencyCount(GetCurrencyFromID(currencyID).ID, false);
        }
    }
}

#endif