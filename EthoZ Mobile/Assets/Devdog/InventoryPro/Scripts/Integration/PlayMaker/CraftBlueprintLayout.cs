#if PLAYMAKER

using Devdog.General;
using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Craft a blueprint.")]
    public class CraftBlueprintLayout : FsmStateAction
    {
        public CraftingLayoutTrigger trigger;
        public CraftingCategory blueprintCategory;
        public CraftingBlueprint blueprint;
        public FsmInt amount;
        public FsmBool result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            result.Value = trigger.progressContainer.AddBlueprintToCraftingQueue(PlayerManager.instance.currentPlayer.inventoryPlayer,
                blueprintCategory, blueprint, amount.Value, trigger.craftingItemsCollection, trigger.craftingRewardItemsCollection);

            Finish();
        }
    }
}

#endif