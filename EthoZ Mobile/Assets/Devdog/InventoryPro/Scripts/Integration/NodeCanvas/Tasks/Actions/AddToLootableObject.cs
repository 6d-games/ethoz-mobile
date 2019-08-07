#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Linq;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class AddToLootableObject : ActionTask{

		[RequiredField]
		public BBParameter<InventoryItemBase> item;
		public BBParameter<int> amount = 1;
		[RequiredField]
		public BBParameter<LootableObject> container;

		protected override string info{
			get {return string.Format("Add {0}x {1} to {2}", amount, item, container);}
		}

		protected override void OnExecute(){
            var items = container.value.items.ToList();
            for(int i = 0; i < amount.value; i++){
	            items.Add(item.value);
            }
            container.value.items = items.ToArray();
			EndAction(true);
		}
	}
}

#endif