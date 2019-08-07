#if BEHAVIOR_DESIGNER

using Devdog.InventoryPro;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.InventorySystem
{
    [TaskCategory(InventoryPro.ProductName)]
    [TaskDescription("Enable or disable a blueprint")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=127")]
    [TaskIcon("Assets/Behavior Designer/Third Party/Inventory Pro/Editor/InventoryProIcon.png")]
    public class CraftingEnableDisableBlueprint : Action
    {
        public SharedInt blueprintID;
        public SharedBool learned;

        public override TaskStatus OnUpdate()
        {
            foreach (var cat in ItemManager.database.craftingCategories)
            {
                foreach (var b in cat.blueprints)
                {
                    if (b.ID == (uint) blueprintID.Value)
                    {
                        b.playerLearnedBlueprint = learned.Value;

                        return TaskStatus.Success;
                    }
                }
            }

            Debug.LogWarning("Error, can't set blueprint with ID " + blueprintID.Value);
            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            blueprintID = 0;
            learned = false;
        }
    }
}

#endif