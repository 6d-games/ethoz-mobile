#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Remove a given amount of slots to a collection.")]
    public class RemoveSlotsFromCollection : FsmStateAction
    {
        public FsmInt amount = 1;
        public FsmBool force = false;
        public ItemCollectionBase collection;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            collection.RemoveSlots((uint)amount.Value, force.Value);
            Finish();
        }
    }
}

#endif