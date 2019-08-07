#if PLY_GAME

using System.Collections.Generic;
using Devdog.General.ThirdParty.UniLinq;
using plyCommon;
using plyGame;
using UnityEngine;
using PlayerManager = Devdog.General.PlayerManager;

namespace Devdog.InventoryPro.Integration.plyGame
{
    public class plyGameStatsProvider : IStatsProvider
    {
        protected virtual List<ActorAttribute> plyAttributes
        {
            get
            {
                if(PlayerManager.instance.currentPlayer == null)
                    return new List<ActorAttribute>();

                var actor = PlayerManager.instance.currentPlayer.GetComponent<Actor>();
                if (actor == null || actor.actorClass == null)
                    return new List<ActorAttribute>();

                return actor.actorClass.attributes;
            }
        }

        public plyGameStatsProvider()
        {

        }

        public Dictionary<string, List<IStat>> Prepare()
        {
            var appendTo = new Dictionary<string, List<IStat>>();

            // Get the stats
            foreach (var stat in ItemManager.database.plyAttributes)
            {
                if (appendTo.ContainsKey(stat.category) == false)
                    appendTo.Add(stat.category, new List<IStat>());

                var plyStat = GetPlyAttribute(stat.ID);
                if (plyStat == null)
                {
                    Debug.Log("Plystat not found " + stat.ID);
                    continue;
                }
                
                // Already in list
                if (appendTo[stat.category].FirstOrDefault(o => o.definition.statName == plyStat.def.screenName) != null)
                {
                    continue;
                }

                var plyStatDef = ScriptableObject.CreateInstance<StatDefinition>();
                plyStatDef.statName = plyStat.def.screenName;
                plyStatDef.valueStringFormat = "{0}";
                plyStatDef.baseValue = plyStat.Value;
                plyStatDef.maxValue = 9999f;
                plyStatDef.showInUI = true;
                plyStatDef.color = Color.white;

                appendTo[stat.category].Add(new Stat(plyStatDef));
            }

            return appendTo;
        }

        protected ActorAttribute GetPlyAttribute(UniqueID id)
        {
            var a = plyAttributes.FirstOrDefault(o => o.id.Value.ToString() == id.Value.ToString());
            if (a == null || a.def == null)
                return null;

            return a;
        }
    }
}

#endif