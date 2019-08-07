#if NODE_CANVAS

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Devdog.InventoryPro.Integration.NodeCanvas{

	[Category("InventoryPro")]
    [Icon("InventoryPro", true)]
	public class EnableDisableCraftingBlueprint : ActionTask{

		public BBParameter<int> blueprintID;
		public BBParameter<bool> enable;

        protected override string info{
            get {return string.Format("BlueprintID {0} learned = {1}", blueprintID, enable);}
        }

		protected override void OnExecute(){
		
            foreach (var cat in ItemManager.database.craftingCategories){
                foreach (var b in cat.blueprints) {
                    if (b.ID == (uint)blueprintID.value) {
                        b.playerLearnedBlueprint = enable.value;
                        EndAction(true);
                        return;
                    }
                }
            }

            UnityEngine.Debug.LogWarning("Error, can't set blueprint with ID " + blueprintID.value);			
			EndAction(false);
		}
	}
}

#endif