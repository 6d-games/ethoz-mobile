#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class SellItemsToVendor : ActionTask{

		[RequiredField]
		public BBParameter<VendorTrigger> vendor;

		protected override string info{
			get {return "Sell items to " + vendor;}
		}

		protected override void OnExecute(){
            var collections = InventoryManager.GetLootToCollections();
            for (var i = 0; i < collections.Length; ++i) {
                var items = collections[i].items;
                for (var j = items.Length - 1; j > -1; --j) {
                    if (items[j].item == null) {
                        continue;
                    }
                    vendor.value.SellItemToVendorNow(items[j].item, items[j].item.currentStackSize);
                }
            }
            EndAction();
		}
	}
}

#endif