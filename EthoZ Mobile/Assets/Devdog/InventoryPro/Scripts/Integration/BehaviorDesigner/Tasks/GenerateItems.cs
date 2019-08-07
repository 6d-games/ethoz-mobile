#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory("InventorySystem")]
    [TaskDescription("Generate a bunch of items that can be stored in a collection.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class GenerateItems : Action
    {
        public SharedInt minAmount, maxAmount;
        
        public InventoryItemBase[] items;

        protected IItemGenerator generator;

        public override void OnAwake()
        {
            base.OnAwake();

            generator = new BasicItemGenerator();
        }


        public override TaskStatus OnUpdate()
        {
            generator.SetItems(ItemManager.database.items);
            generator.Generate(minAmount.Value, maxAmount.Value);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            minAmount = 0;
            maxAmount = 0;
            items = null;
        }
    }
}

#endif