#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Set items on a lootable object, replaces the old items that were in the collection before it.")]
    public class SetItemsLootableObject : FsmStateAction
    {
        public InventoryItemBase[] items;
        public LootableObject lootable;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            lootable.items = items;
            Finish();
        }
    }
}

#endif