#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Cancel the current blueprint that is crafting.")]
    public class CancelCraftBlueprintLayout : FsmStateAction
    {
        public CraftingLayoutTrigger trigger;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            trigger.progressContainer.CancelActiveCraft();
            Finish();
        }
    }
}

#endif