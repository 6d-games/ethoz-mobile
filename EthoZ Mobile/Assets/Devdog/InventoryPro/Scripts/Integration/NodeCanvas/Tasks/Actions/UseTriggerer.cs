#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Devdog.General;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[ParadoxNotion.Design.Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class UseTriggerer : ActionTask{

		[RequiredField]
		public BBParameter<Trigger> triggerer;
		[RequiredField]
		public BBParameter<bool> use;

		protected override string info{
			get {return string.Format("Use {0} ({1})", triggerer, use);}
		}

		protected override void OnExecute(){
			if (use.value){
				triggerer.value.Use();
			} else {
				triggerer.value.UnUse();
			}
			EndAction();
		}
	}
}

#endif