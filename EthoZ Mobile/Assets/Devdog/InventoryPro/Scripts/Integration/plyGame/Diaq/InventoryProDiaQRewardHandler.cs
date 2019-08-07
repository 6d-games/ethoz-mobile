#if PLY_GAME

using plyCommon;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.DiaQ
{
    public class InventoryProDiaQRewardHandler : MonoBehaviour, plyDataProviderInterface
    {

        public void DataProvider_Callback(string[] nfo)
        {
            // this is what gets called when DiaQuest.RunRewardGivers()
            // is called and this provider is chosen for handling
            // a reward entry in the quest. Note that RunRewardGivers()
            // will append the reward value to the nfo array so the 
            // last entry in this array will be the value as entered
            // in the quest editor. The other nfo entries are as you
            // set them up in the Info (editor) class. DiaQSampleRewardGiverInfo

            // I know that I defined the following about nfo
            // nfo[0] = 0:Currency, 1:InventoryPro Item
            // nfo[1] = ID of item / ID of currency
            // nfo[2] = the value (amount of items, or amount of curency)

            if (nfo[0] == "0")
            {
                var currencyID = int.Parse(nfo[1]);
                if (currencyID < 0)
                    return; // Not a currencyID

                InventoryManager.AddCurrency((uint)currencyID, float.Parse(nfo[3]));

                // Give player 
                Debug.Log("(DiaQ) Player received: " + nfo[3] + " " + nfo[2]);
            }
            else if (nfo[0] == "1")
            {
                var itemID = int.Parse(nfo[1]);
                if (itemID < 0)
                    return; // Not an itemID

                var item = GameObject.Instantiate<InventoryItemBase>(ItemManager.database.items[itemID]);
                item.currentStackSize = uint.Parse(nfo[3]);
                
                InventoryManager.AddItem(item);
            }
        }

        public object DataProvider_GetValue(string[] nfo)
        {
            Debug.Log("Inventory pro data provider get");
            // not used in this context but needed by plyDataProviderInterface
            return null;
        }

        public void DataProvider_SetValue(string[] nfo, object value)
        {
            Debug.Log("Inventory pro data provider set");
            // not used in this context but needed by plyDataProviderInterface
        }
    }
}

#endif