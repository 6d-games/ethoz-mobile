#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class UseItem : ActionTask{

		[RequiredField]
		public BBParameter<InventoryItemBase> item;

		protected override string info{
			get {return "Use " + item;}
		}

		protected override void OnExecute(){
            item.value.Use();
			EndAction(true);
		}
	}
}

#endif