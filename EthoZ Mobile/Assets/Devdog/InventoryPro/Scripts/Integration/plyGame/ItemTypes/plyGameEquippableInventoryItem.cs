#if PLY_GAME

using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using plyGame;

namespace Devdog.InventoryPro.Integration.plyGame
{
    public partial class plyGameEquippableInventoryItem : EquippableInventoryItem
    {
        public plyGameAttributeModifierModel[] plyAttributes = new plyGameAttributeModifierModel[0];


        public override LinkedList<ItemInfoRow[]> GetInfo()
        {
            var info = base.GetInfo();

            var attributes = new ItemInfoRow[plyAttributes.Length];
            for (int i = 0; i < plyAttributes.Length; i++)
            {
                var a = Player.Instance.actor.actorClass.attributes.FirstOrDefault(attribute => attribute.id.Value == plyAttributes[i].id.Value);
                if (a != null)
                    attributes[i] = new ItemInfoRow(a.def.screenName, plyAttributes[i].toAdd.ToString(), plyAttributes[i].color, plyAttributes[i].color);
            }

            info.AddAfter(info.First, attributes.ToArray());

            return info;
        }

        public override bool CanEquip(ICharacterCollection equipTo)
        {
            bool can = base.CanEquip(equipTo);
            if (can == false)
                return false;

            if (Player.Instance.actor.actorClass.currLevel < requiredLevel)
            {
                InventoryManager.langDatabase.itemCannotBeUsedLevelToLow.Show(name, description, requiredLevel);
                return false;
            }

            return true;
        }

        public override void NotifyItemEquipped(EquippableSlot equipSlot, uint amountEquipped)
        {
            base.NotifyItemEquipped(equipSlot, amountEquipped);
            SetPlyGameValues(1.0f);
        }


        public override void NotifyItemUnEquipped(ICharacterCollection equipTo, uint amountUnEquipped)
        {
            base.NotifyItemUnEquipped(equipTo, amountUnEquipped);

            SetPlyGameValues(-1.0f);
        }

        protected virtual void SetPlyGameValues(float multiplier)
        {
            foreach (var attr in plyAttributes)
            {
                var a = Player.Instance.actor.actorClass.attributes.FirstOrDefault(attribute => attribute.id.Value == attr.id.Value);
                if (a != null)
                {
                    a.lastInfluence = gameObject;
                    var b = (int)(attr.toAdd*multiplier);
                    a.ChangeSimpleBonus(b);
                    if (b > 0f)
                    {
                        a.SetConsumableValue(a.ConsumableValue + b, gameObject);
                    }
                }
            }
        }
    }
}

#endif