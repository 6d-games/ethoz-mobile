#if PLY_GAME__

using System;
using System.Collections.Generic;
using plyBloxKit;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.plyGame.plyBlox
{
    [plyBlock("InventorySystem", "Collections", "Set items", BlockType.Action, ReturnValueType = typeof(SetItemsCollection))]
    public class SetItemsCollection : plyBlock
    {
        [plyBlockField("Items", ShowName = true, ShowValue = true, DefaultObject = typeof(InventoryItemBase[]), EmptyValueName = "-error-", SubName = "InventorySystem item", Description = "Item to pickup, make sure it's a scene object, and not a prefab.")]
        public InventoryItemBase[] items;

        [plyBlockField("Collection", ShowName = true, ShowValue = true, DefaultObject = typeof(ItemCollectionBase), EmptyValueName = "-error-", SubName = "InventorySystem collection", Description = "The collection you wish to add the item to")]
        public ItemCollectionBase collection;


        public SetItemsCollection()
        {
            
        }

        public override void Created()
        {
            blockIsValid = items != null && collection != null;
        }

        public override BlockReturn Run(BlockReturn param)
        {
            collection.SetItems(items.ToArray(), true);
            return BlockReturn.OK;
        }
    }
}

#endif