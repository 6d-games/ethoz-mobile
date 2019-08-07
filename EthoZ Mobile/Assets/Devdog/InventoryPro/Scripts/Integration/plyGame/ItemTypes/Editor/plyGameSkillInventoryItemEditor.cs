#if PLY_GAME

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using Devdog.General.Editors;
using Devdog.InventoryPro.Editors;
using Devdog.InventoryPro.Integration.plyGame;
using Devdog.InventoryPro;
using plyCommon;
using plyGame;
using plyGameEditor;
using EditorUtility = UnityEditor.EditorUtility;

namespace Devdog.InventoryPro.Integration.plyGame.Editors
{
    [CustomEditor(typeof(plyGameSkillInventoryItem), true)]
    public class plyGameSkillInventoryItemEditor : InventoryItemBaseEditor
    {
        
        public override void OnEnable()
        {
            base.OnEnable();
            
        }

        protected override void OnCustomInspectorGUI(params CustomOverrideProperty[] extraOverride)
        {
            var t = (plyGameSkillInventoryItem)target;

            var l = new List<CustomOverrideProperty>(extraOverride);
            l.Add(new CustomOverrideProperty("skill", () =>
            {
                EditorGUILayout.BeginHorizontal();

                GUILayout.Label("plyGame skill", GUILayout.Width(EditorGUIUtility.labelWidth));
                ObjectPickerUtility.RenderObjectPickerForType<Skill>("", t.skill, skill =>
                {
                    t.skill = skill;
                    GUI.changed = true;
                });

                EditorGUILayout.EndHorizontal();
            }));

            base.OnCustomInspectorGUI(l.ToArray());
        }
    }
}

#endif