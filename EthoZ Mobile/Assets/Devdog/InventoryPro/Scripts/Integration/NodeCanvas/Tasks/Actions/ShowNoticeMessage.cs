#if NODE_CANVAS

using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
	[Icon("InventoryPro", true)]
	public class ShowNoticeMessage : ActionTask{

		public BBParameter<string> title            = "title";
		public BBParameter<string> message          = "message";
		public BBParameter<Color> color             = Color.white;
		public BBParameter<NoticeDuration> duration = NoticeDuration.Medium;
		public BBParameter<InventoryItemBase> item;

		protected override string info{
			get {return string.Format("Show Notice {0} ({1})", title, item.isNone? "" : item.ToString());}
		}

		protected override void OnExecute(){
			var m = new InventoryNoticeMessage(title.value, message.value, duration.value, color.value);
			if (item.value != null) m.Show(item.value.name, item.value.description);
			else m.Show();
			EndAction(true);
		}
	}
}

#endif