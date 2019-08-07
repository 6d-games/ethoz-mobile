#if PLY_GAME

using System.Collections;
using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using Devdog.InventoryPro.Integration.plyGame.plyBlox;
using plyGame;
using UnityEngine;
using PlayerManager = Devdog.General.PlayerManager;

namespace Devdog.InventoryPro.Integration.plyGame
{
    [AddComponentMenu(InventoryPro.AddComponentMenuPath + "Integration/plyGame/ply Inventory player")]
    public partial class plyInventoryPlayer : InventoryPlayer
    {
        protected virtual List<ActorAttribute> plyAttributes
        {
            get
            {
                if (PlayerManager.instance.currentPlayer == null)
                    return new List<ActorAttribute>();

                var actor = PlayerManager.instance.currentPlayer.GetComponent<Actor>();
                if (actor == null || actor.actorClass == null)
                    return new List<ActorAttribute>();

                return actor.actorClass.attributes;
            }
        }

        private Actor _actor;
        public Actor actor
        {
            get
            {
                if (_actor == null)
                    _actor = gameObject.GetComponent<Actor>();

                return _actor;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            // Pass the data to plyBlox
            gameObject.AddComponent<InventoriesCollectionEventsProxy>();
            gameObject.AddComponent<CharactersCollectionEventsProxy>();
            gameObject.AddComponent<VendorCollectionEventsProxy>();
            gameObject.AddComponent<CraftingCollectionEventsProxy>();

            StartCoroutine(RegisterPlyAttributeListeners());
        }

        public override void Init()
        {
            stats.dataProviders.Add(new plyGameStatsProvider());
            base.Init();
        }

        private IEnumerator RegisterPlyAttributeListeners()
        {
            yield return null; // Wait for plyGame to initialize.
            yield return null;

            var attributes = this.plyAttributes;
            foreach (var attr in attributes)
            {
                var a = actor.actorClass.attributes.FirstOrDefault(attribute => attribute.id.Value == attr.id.Value);
                if (a != null)
                {
                    a.RegisterChangeListener(AttributeChangeCallback);
                }
            }
        }

        private void AttributeChangeCallback(object sender, object[] args)
        {
            var attr = (ActorAttribute) sender;
            var invProAttr = ItemManager.database.plyAttributes.FirstOrDefault(o => o.ID == attr.id);

            var player = PlayerManager.instance.currentPlayer;
            if (player != null && invProAttr != null)
            {
                var stat = player.inventoryPlayer.stats.Get(invProAttr.category, attr.def.screenName);
                if (stat != null)
                {
                    stat.SetFactor(1f, false);
                    stat.SetFactorMax(1f, false, false);
                    stat.SetMaxValueRaw(attr.data.baseValue + attr.BonusValue, false, false);
                    stat.SetCurrentValueRaw(attr.ConsumableValue);
                }
            }
        }
    }
}

#endif