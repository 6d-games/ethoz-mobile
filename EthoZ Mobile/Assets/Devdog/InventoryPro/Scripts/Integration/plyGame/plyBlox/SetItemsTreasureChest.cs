#if PLY_GAME__

using System;
using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("InventorySystem", "Collections", "Set items treasure chest", BlockType.Action, ReturnValueType = typeof(SetItemsTreasureChest))]
    public class SetItemsTreasureChest : plyBlock
    {
        [plyBlockField("Items", ShowName = true, ShowValue = true, DefaultObject = typeof(List<InventoryItemBase>), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "Items to set in treasure chest.")]
        public List<InventoryItemBase> items;

        [plyBlockField("Collection", ShowName = true, ShowValue = true, DefaultObject = typeof(TreasureChest), EmptyValueName = "-error-", SubName = "Treasure chest", Description = "Treasure chest.")]
        public TreasureChest treasure;


        public override void Created()
        {
            blockIsValid = items != null && treasure != null;
        }

        public override BlockReturn Run(BlockReturn param)
        {
            treasure.items = items.ToArray();
            return BlockReturn.OK;
        }
    }
}

#endif