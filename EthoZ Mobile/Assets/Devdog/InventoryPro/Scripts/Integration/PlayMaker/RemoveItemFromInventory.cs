#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Remove an item from the player's inventory.")]
    public class RemoveItemFromInventory : FsmStateAction
    {
        public InventoryItemBase item;
        public FsmInt amount = 1;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            InventoryManager.RemoveItem(item.ID, (uint)amount.Value, false);
            Finish();
        }
    }
}

#endif