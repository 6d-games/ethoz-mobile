#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Add a given amount of slots to a collection.")]
    public class AddSlotsToCollection : FsmStateAction
    {
        public FsmInt amount = 1;
        public ItemCollectionBase collection;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            collection.AddSlots((uint)amount.Value);
            Finish();
        }
    }
}

#endif