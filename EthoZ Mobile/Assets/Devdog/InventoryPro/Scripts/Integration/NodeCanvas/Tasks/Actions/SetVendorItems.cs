#if NODE_CANVAS

using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class SetVendorItems : ActionTask{

		[RequiredField]
		public BBParameter<VendorTrigger> vendor;
		public BBParameter<List<InventoryItemBase>> items;

		protected override string info{
			get {return string.Format("Set vendor {0} items to {1}", vendor, items);}
		}

		protected override void OnExecute(){
            vendor.value.items = items.value.ToArray();
			EndAction(true);
		}
	}
}

#endif