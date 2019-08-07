#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Toggle a trigger.")]
    public class ToggleTrigger : FsmStateAction
    {
        public TriggerBase trigger;
        public FsmBool useOrUnUse;
        
        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            trigger.Toggle();
            Finish();
        }
    }
}

#endif