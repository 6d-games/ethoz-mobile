#if PLY_GAME

using System;
using System.Collections.Generic;
using Devdog.General;
using Devdog.General.ThirdParty.UniLinq;
using Player = plyGame.Player;
using PlayerManager = Devdog.General.PlayerManager;

namespace Devdog.InventoryPro.Integration.plyGame
{
    public partial class plyGameConsumableInventoryItem : InventoryItemBase
    {
        public plyGameAttributeModifierModel[] plyAttributes = new plyGameAttributeModifierModel[0];
        public General.AudioClipInfo audioClipWhenUsed;

        public override LinkedList<ItemInfoRow[]> GetInfo()
        {
            var info = base.GetInfo();

            var attributes = new ItemInfoRow[plyAttributes.Length];
            for (int i = 0; i < plyAttributes.Length; i++)
            {
                var a = Player.Instance.actor.actorClass.attributes.FirstOrDefault(attribute => attribute.id.Value == plyAttributes[i].id.Value);
                if(a != null)
                    attributes[i] = new ItemInfoRow(a.def.screenName, plyAttributes[i].toAdd.ToString(), plyAttributes[i].color, plyAttributes[i].color);
            }

            info.AddAfter(info.First, attributes.ToArray());

            return info;
        }

        public override int Use()
        {
            int used = base.Use();
            if (used < 0)
                return used;

            if (Player.Instance.actor.actorClass.currLevel < requiredLevel)
            {
                InventoryManager.langDatabase.itemCannotBeUsedLevelToLow.Show(name, description, requiredLevel);
                return -1;
            }
            
            SetItemProperties(PlayerManager.instance.currentPlayer.inventoryPlayer, InventoryItemUtility.SetItemPropertiesAction.Use);

            AudioManager.AudioPlayOneShot(audioClipWhenUsed);

            currentStackSize--;
            NotifyItemUsed(1, true);
            return 1;
        }

        protected virtual void SetItemProperties(InventoryPlayer player, InventoryItemUtility.SetItemPropertiesAction action)
        {
            switch (action)
            {
                case InventoryItemUtility.SetItemPropertiesAction.Use:
                    player.stats.SetAll(stats);

                    break;
                case InventoryItemUtility.SetItemPropertiesAction.UnUse:
                    player.stats.SetAll(stats, -1f);

                    break;
                default:
                    throw new ArgumentOutOfRangeException("action", action, null);
            }

            SetPlyGameValues(player);
        }

        protected virtual void SetPlyGameValues(InventoryPlayer player)
        {
            foreach (var attr in plyAttributes)
            {
                var a = Player.Instance.actor.actorClass.attributes.FirstOrDefault(attribute => attribute.id.Value == attr.id.Value);
                if (a != null)
                {
                    if (attr.addToBonus)
                    {
                        a.lastInfluence = gameObject;
                        a.ChangeSimpleBonus(attr.toAdd);
                    }
                    else
                    {
                        a.lastInfluence = gameObject;
                        a.SetConsumableValue(a.ConsumableValue + attr.toAdd, gameObject);
                    }
                }
            }
        }
    }
}

#endif