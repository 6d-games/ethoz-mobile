#if PLAYMAKER

using HutongGames.PlayMaker;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [ActionCategory(InventoryPro.ProductName)]
    [HutongGames.PlayMaker.Tooltip("Enable or disable a blueprint")]
    public class CraftingEnableDisableBlueprint : FsmStateAction
    {
        public FsmObject blueprint; // TODO: Use blueprints instead of blueprintID
        public FsmInt blueprintID;
        public FsmBool learned;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            foreach (var cat in ItemManager.database.craftingCategories)
            {
                foreach (var b in cat.blueprints)
                {
                    if (b.ID == (uint) blueprintID.Value)
                    {
                        b.playerLearnedBlueprint = learned.Value;
                        Finish();

                        return;
                    }
                }
            }

            Debug.LogWarning("Error, can't set blueprint with ID " + blueprintID.Value);
        }
    }
}

#endif