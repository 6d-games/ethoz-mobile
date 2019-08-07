#if PLAYMAKER

using HutongGames.PlayMaker;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Resize a collection.")]
    public class ResizeCollection : FsmStateAction
    {
        public FsmInt size = 10;
        public FsmBool force = false;
        public ItemCollectionBase collection;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            collection.Resize((uint)size.Value, force.Value);
            Finish();
        }
    }
}

#endif