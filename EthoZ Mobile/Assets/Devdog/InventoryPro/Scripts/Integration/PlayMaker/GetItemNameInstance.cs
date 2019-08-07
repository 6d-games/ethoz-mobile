#if PLAYMAKER__

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;


namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Get the item's name.")]
    public class GetItemNameInstance : FsmStateAction
    {
        public FsmObject item;
        public FsmString result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var i = item.Value as InventoryItemBase;
            if (i != null)
            {
                result.Value = i.name;
                Log("Got name " + result.Value);
            }
            else
            {
                if (item.Value != null)
                {
                    LogError("The variable in GetItemNameInstance is not an Inventory Pro item.");
                }
                else
                {
                    LogWarning("The variable in GetItemNameInstance is empty.");
                }
            }

            Finish();
        }
    }
}

#endif