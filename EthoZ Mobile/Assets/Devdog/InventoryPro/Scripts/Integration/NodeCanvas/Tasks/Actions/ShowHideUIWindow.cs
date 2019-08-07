#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Devdog.General.UI;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class ShowHideUIWindow : ActionTask{

		[RequiredField]
		public BBParameter<UIWindow> window;
		public BBParameter<bool> show = true;

		protected override string info{
			get {return string.Format("{0} UIWindow {1}", show.value? "Show" : "Hide", window);}
		}

		protected override void OnExecute(){
            if (show.value) window.value.Show();
            else window.value.Hide();
			EndAction(true);
		}
	}
}

#endif