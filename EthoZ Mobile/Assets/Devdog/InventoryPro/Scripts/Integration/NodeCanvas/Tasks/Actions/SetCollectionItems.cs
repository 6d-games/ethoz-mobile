#if NODE_CANVAS

using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class SetCollectionItems : ActionTask{

		[RequiredField]
		public BBParameter<ItemCollectionBase> collection;
		public BBParameter<List<InventoryItemBase>> items;

		protected override string info{
			get {return string.Format("Set collection {0} items to {1}", collection, items);}
		}

		protected override void OnExecute(){
            collection.value.SetItems(items.value.ToArray(), true);
			EndAction(true);
		}
	}
}

#endif