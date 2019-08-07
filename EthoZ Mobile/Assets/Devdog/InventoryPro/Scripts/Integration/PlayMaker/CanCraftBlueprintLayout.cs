#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Check if the player can craft an item.")]
    public class CanCraftBlueprintLayout : FsmStateAction
    {
        public CraftingLayoutTrigger trigger;
        public CraftingCategory blueprintCategory;
        public CraftingBlueprint blueprint;
        public FsmBool result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            result.Value = trigger.CanCraftBlueprint(PlayerManager.instance.currentPlayer.inventoryPlayer, new CraftingProgressContainer.CraftInfo(null, trigger, null, blueprintCategory, blueprint));
            Finish();
        }
    }
}

#endif