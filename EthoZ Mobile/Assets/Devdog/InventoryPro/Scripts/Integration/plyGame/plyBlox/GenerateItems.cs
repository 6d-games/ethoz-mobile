#if PLY_GAME__

using System;
using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using plyBloxKit;
using UnityEditor;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("InventorySystem", "Items", "Generate items", BlockType.Action, Description = "Generate an array of items.")]
    public class GenerateItems : plyBlock
    {
        public Int_Value minAmount, maxAmount;
        public InventoryItemBase[] items;

        protected IItemGenerator generator;

        public override void Created()
        {
            blockIsValid = true;
        }

        public override BlockReturn Run(BlockReturn param)
        {
            generator = new BasicItemGenerator();

#if UNITY_EDITOR
            var m = Editor.FindObjectOfType<ItemManager>();
            generator.SetItems(m.items);
#else
            generator.SetItems(ItemManager.database.items);
#endif
            this.items = generator.Generate(minAmount.value, maxAmount.value);
            

            return BlockReturn.OK;
        }
    }
}

#endif