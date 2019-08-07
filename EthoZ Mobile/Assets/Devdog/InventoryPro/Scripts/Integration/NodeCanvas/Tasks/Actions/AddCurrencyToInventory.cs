#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class AddCurrencyToInventory : ActionTask{

		public BBParameter<int> currencyID;
		public BBParameter<float> amount = 1f;

		protected override string info{
			get {return string.Format("Add {0} Currency({1}) to Inventory", amount, currencyID);}
		}

		protected override void OnExecute(){
			InventoryManager.AddCurrency((uint)currencyID.value, amount.value);
			EndAction(true);
		}
	}
}

#endif