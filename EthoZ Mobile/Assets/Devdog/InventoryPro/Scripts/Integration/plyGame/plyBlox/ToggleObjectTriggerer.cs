#if PLY_GAME

using Devdog.General;
using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Actions", "Toggle Object triggerHandler", BlockType.Action)]
    public class ToggleObjectTrigger : plyBlock
    {
        [plyBlockField("Trigger", ShowName = true, ShowValue = true, DefaultObject = typeof(Trigger), EmptyValueName = "-error-", SubName = "Object Trigger", Description = "The triggerHandler you wish to use / unuse")]
        public Trigger trigger;

        public override void Created()
        {
            blockIsValid = (trigger != null);

            if (trigger == null)
                Log(LogType.Error, "Trigger is empty");
        }

        public override BlockReturn Run(BlockReturn param)
        {
            trigger.Toggle();
            return BlockReturn.OK;
        }
    }
}

#endif