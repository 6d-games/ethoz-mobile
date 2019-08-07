#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class AddToCollection : ActionTask{

		[RequiredField]
		public BBParameter<InventoryItemBase> item;
		public BBParameter<int> amount = 1;
		[RequiredField]
		public BBParameter<ItemCollectionBase> collection;

		protected override string info{
			get {return string.Format("Add {0}x {1} to {2}", amount, item, collection);}
		}

		protected override void OnExecute(){
            item.value.currentStackSize = (uint)amount.value;
            collection.value.AddItem(item.value);
			EndAction(true);
		}
	}
}

#endif