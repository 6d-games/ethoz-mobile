#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Toggle a trigger.")]
    public class ToggleTriggerFsmObject : FsmStateAction
    {
        public FsmObject trigger;
        public FsmBool useOrUnUse;
        
        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var t = trigger.Value as TriggerBase;
            if (t != null)
            {
                t.Toggle();
            }

            Finish();
        }
    }
}

#endif