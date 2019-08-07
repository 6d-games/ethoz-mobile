#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class DropItem : ActionTask{

		[RequiredField]
		public BBParameter<InventoryItemBase> item;

		protected override string info{
			get {return "Drop " + item;}
		}

		protected override void OnExecute(){
            try
            {
            	item.value.itemCollection[item.value.index].TriggerDrop();
				EndAction(true);
            }
            catch
            {
            	UnityEngine.Debug.LogError("Item has no collection");
            	EndAction(false);
            }
		}
	}
}

#endif