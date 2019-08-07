#if PLAYMAKER

using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.InventoryPro.Integration.PlayMaker
{

    /// <summary>
    /// Relays all events to plyGame's plyBox
    /// </summary>
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/PlayMaker/Item collection event proxy")]
    public partial class ItemCollectionPlayMakerEventProxy : MonoBehaviour
    {
        [Header("Uses this object when empty")]
        [SerializeField]
        private PlayMakerFSM _fsm;

        public PlayMakerFSM fsm
        {
            get
            {
                if (_fsm != null)
                    return _fsm;

                return GetComponent<PlayMakerFSM>();
            }
        }


        // <inheritdoc />
        protected void Start()
        {
            var c = GetComponent<ItemCollectionBase>();

            c.OnAddedItem += OnAddedItemPlayMaker;
//            c.OnAddedItemCollectionFull += OnAddedItemCollectionFullPlayMaker;
            c.OnDroppedItem += OnDroppedItemPlayMaker;
            c.OnRemovedItem += OnRemovedItemPlayMaker;
            c.OnRemovedReference += OnRemovedReferencePlayMaker;
            c.OnResized += OnResizedPlayMaker;
            c.OnSorted += OnSortedPlayMaker;
            c.OnUsedItem += OnUsedItemPlayMaker;
            c.OnUsedReference += OnUsedReferencePlayMaker;
        }


        protected void OnDestroy()
        {
            var c = GetComponent<ItemCollectionBase>();

            c.OnAddedItem -= OnAddedItemPlayMaker;
//            c.OnAddedItemCollectionFull -= OnAddedItemCollectionFullPlayMaker;
            c.OnDroppedItem -= OnDroppedItemPlayMaker;
            c.OnRemovedItem -= OnRemovedItemPlayMaker;
            c.OnRemovedReference -= OnRemovedReferencePlayMaker;
            c.OnResized -= OnResizedPlayMaker;
            c.OnSorted -= OnSortedPlayMaker;
            c.OnUsedItem -= OnUsedItemPlayMaker;
            c.OnUsedReference -= OnUsedReferencePlayMaker;
        }

        private void _FireEvent(string name)
        {
            if (fsm != null)
            {
                fsm.SendEvent("INV_PRO/" + name);
            }
        }

        private void FireEvent(string name, InventoryItemBase data = null)
        {
            if (data != null)
            {
                if (fsm != null)
                {
                    var i = fsm.FsmVariables.FindFsmObject("INV_PRO/Event_Item");
                    Assert.IsNotNull(i, "No FSM Variable found with name INV_PRO/Event_Item");
                    i.Value = data;
                }
            }

            _FireEvent(name);
        }

        private void FireEvent(string name, int val)
        {
            if (fsm != null)
            {
                var i = fsm.FsmVariables.FindFsmInt("INV_PRO/Event_Int");
                Assert.IsNotNull(i, "No FSM Variable found with name INV_PRO/Event_Int");
                i.Value = val;
            }

            _FireEvent(name);
        }


        private void OnAddedItemPlayMaker(IEnumerable<InventoryItemBase> items, uint amount, bool cameFromCollection)
        {
            FireEvent("OnAddedItem", items.FirstOrDefault());
        }

        private void OnUsedReferencePlayMaker(InventoryItemBase actualItem, uint itemID, uint referenceSlot, uint amountUsed)
        {
            FireEvent("OnUsedReference", actualItem);
        }

        private void OnUsedItemPlayMaker(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {
            FireEvent("OnUsedItem", item);
        }

        private void OnSortedPlayMaker()
        {
            FireEvent("OnSorted");
        }

        private void OnResizedPlayMaker(uint fromSize, uint toSize)
        {
            FireEvent("OnResized", (int)toSize);
        }

        private void OnRemovedReferencePlayMaker(InventoryItemBase item, uint slot)
        {
            FireEvent("OnRemovedReference", item);
        }

        private void OnRemovedItemPlayMaker(InventoryItemBase item, uint itemID, uint slot, uint amount)
        {
            FireEvent("OnRemovedItem", item);
        }

        private void OnDroppedItemPlayMaker(InventoryItemBase item, uint slot, GameObject droppedObj)
        {
            FireEvent("OnDroppedItem", item);
        }

//        private void OnAddedItemCollectionFullPlayMaker(InventoryItemBase item, bool cameFromCollection)
//        {
//            FireEvent("OnAddedItemCollectionFull", item);
//        }
    }
}

#endif