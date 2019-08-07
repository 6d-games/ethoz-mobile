#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Use a trigger.")]
    public class UseTrigger : FsmStateAction
    {
        public TriggerBase trigger;
        public FsmBool useOrUnUse;
        
        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            if (useOrUnUse.Value)
            {
                trigger.Use();
            }
            else
            {
                trigger.UnUse();
            }

            Finish();
        }
    }
}

#endif