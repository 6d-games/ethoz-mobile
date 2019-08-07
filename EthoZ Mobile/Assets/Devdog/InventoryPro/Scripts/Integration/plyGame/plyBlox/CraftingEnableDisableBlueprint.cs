#if PLY_GAME

using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("Inventory Pro", "Crafting", "Enable / disable a blueprint", BlockType.Action)]
    public class CraftingEnableDisableBlueprint : plyBlock
    {
        [plyBlockField("Blueprint ID", ShowName = true, ShowValue = true, DefaultObject = typeof(Int_Value), Description = "ID of the blueprint you're changing.")]
        public Int_Value blueprintID;

        [plyBlockField("Blueprint learned", ShowName = true, ShowValue = true, DefaultObject = typeof(Bool_Value), Description = "Learn or unlearn the blueprint?")]
        public Bool_Value learned;

        public override void Created()
        {
            blockIsValid = true;
            //base.Create();
        }

        public override BlockReturn Run(BlockReturn param)
        {
            foreach (var cat in ItemManager.database.craftingCategories)
            {
                foreach (var b in cat.blueprints)
                {
                    if (b.ID == (uint)blueprintID.value)
                    {
                        b.playerLearnedBlueprint = learned.value;

                        return BlockReturn.OK;
                    }
                }
            }


            Debug.LogWarning("Error, can't set blueprint with ID " + blueprintID.value);
            return BlockReturn.Error;
        }
    }
}

#endif