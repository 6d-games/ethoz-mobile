#if PLAYMAKER

using HutongGames.PlayMaker;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Use a given item.")]
    public class UseThisItem : FsmStateAction
    {
       
        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            this.Owner.SendMessage("Use", SendMessageOptions.RequireReceiver);
            Finish();
        }
    }
}

#endif