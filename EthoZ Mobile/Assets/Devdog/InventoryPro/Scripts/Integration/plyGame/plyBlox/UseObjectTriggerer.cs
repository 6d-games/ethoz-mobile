#if PLY_GAME

using Devdog.General;
using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Actions", "Use Object Trigger", BlockType.Action)]
    public class UseObjectTrigger : plyBlock
    {
        [plyBlockField("Trigger", ShowName = true, ShowValue = true, DefaultObject = typeof(TriggerBase), EmptyValueName = "-error-", SubName = "Object Trigger", Description = "The triggerHandler you wish to use / unuse")]
        public TriggerBase trigger;

        [plyBlockField("Action", ShowName = true, ShowValue = true, DefaultObject = typeof(Bool_Value), SubName = "Action", Description = "The action on the triggerHandler")]
        public Bool_Value action;

        public override void Created()
        {
            blockIsValid = (trigger != null);

            if (trigger == null)
                Log(LogType.Error, "Trigger is empty");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            if (action.value)
                trigger.Use();
            else
                trigger.UnUse();

            return BlockReturn.OK;
        }
    }
}

#endif