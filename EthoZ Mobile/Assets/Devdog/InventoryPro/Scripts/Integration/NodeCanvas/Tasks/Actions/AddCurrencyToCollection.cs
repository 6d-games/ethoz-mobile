#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class AddCurrencyToCollection : ActionTask{

		[RequiredField]
		public BBParameter<ItemCollectionBase> collection;
		public BBParameter<int> currencyID;
		public BBParameter<float> amount = 1f;

		protected override string info{
			get {return string.Format("Add {0} Currency({1}) to {2}", amount, currencyID, collection);}
		}

		protected override void OnExecute(){
			collection.value.AddCurrency((uint)currencyID.value, amount.value);
			EndAction(true);
		}
	}
}

#endif