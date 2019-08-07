#if NODE_CANVAS

using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class SetTreasureChestItems : ActionTask{

		[RequiredField]
		public BBParameter<LootableObject> chest;
		public BBParameter<List<InventoryItemBase>> items;

		protected override string info{
			get {return string.Format("Set chest {0} items to {1}", chest, items);}
		}

		protected override void OnExecute(){
            chest.value.items = items.value.ToArray();
			EndAction(true);
		}
	}
}

#endif