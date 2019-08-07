#if PLY_GAME

using System;
using System.Collections.Generic;
using System.Linq;
using Devdog.General.Editors;
using plyCommon;
using plyGame;
using plyGameEditor;
using UnityEditor;
using UnityEngine;
using EditorStyles = Devdog.General.Editors.EditorStyles;
using EditorUtility = UnityEditor.EditorUtility;

namespace Devdog.InventoryPro.Integration.plyGame.Editors
{
    public class plyStatsEditor : IEditorCrud
    {
        protected List<ActorAttribute> plyAttributes
        {
            get
            {
                var assets = EdGlobal.GetDataAsset().assets;
                var attributesEditor = (ActorAttributesAsset)assets.FirstOrDefault(o => o.name == "attributes");
                if (attributesEditor == null)
                    return new List<ActorAttribute>(); // No attribute editor tab found??

                return attributesEditor.attributes;
            }
        }

        protected string[] attributresStrings
        {
            get
            {
                var att = plyAttributes;

                string[] attributes = new string[att.Count];
                for (int i = 0; i < att.Count; i++)
                    attributes[i] = att[i].def.screenName;

                return attributes;
            }
        }

        protected plyGameAttributeDatabaseModel[] attributes
        {
            get
            {
                var atts = plyAttributes;
                var arr = new plyGameAttributeDatabaseModel[atts.Count];
                for (int i = 0; i < arr.Length; i++)
                {
                    if (atts[i] != null)
                        arr[i] = new plyGameAttributeDatabaseModel(new UniqueID(atts[i].id.Value.ToString()), "Default", true);
                }

                return arr;
            }
        }


        public string name { get; set; }
        public EditorWindow window { get; protected set; }
        private UnityEditorInternal.ReorderableList resultList { get; set; }
        private Vector2 statsScrollPos;
        public bool requiresDatabase { get; set; }

        public plyStatsEditor(string name, EditorWindow window)
        {
            this.name = name;
            this.window = window;
            this.requiresDatabase = true;

            resultList = new UnityEditorInternal.ReorderableList(attributes, typeof(plyGameAttributeDatabaseModel), false, true, false, false);
            resultList.drawHeaderCallback += rect =>
            {
                var r = rect;
                r.width = 40;
                r.x += 15; // Little offset on the start

                EditorGUI.LabelField(r, "Show");


                var r2 = rect;
                r2.width -= 50;
                r2.width /= 3;
                r2.x += 50;

                EditorGUI.LabelField(r2, "Display name");

                r2.x += r2.width;
                EditorGUI.LabelField(r2, "Category");

                r2.x += r2.width;
                EditorGUI.LabelField(r2, "Formatter");
            };
            //resultList.elementHeight = 30;
            resultList.drawElementCallback += (rect, index, active, focused) =>
            {
                rect.height = 16;
                rect.y += 2;

                if (index >= ItemManager.database.plyAttributes.Length)
                    return;

                var stat = ItemManager.database.plyAttributes[index];


                var r2 = rect;
                r2.width -= 50;
                r2.width /= 3;
                r2.width -= 10;
                r2.x += 50;

                var r = rect;
                r.width = 40;
                r.x += 15;
                stat.show = EditorGUI.Toggle(r, stat.show);

                GUI.enabled = stat.show;

                if (index >= 0 && index <= attributresStrings.Length)
                    EditorGUI.LabelField(r2, attributresStrings[index]);

                r2.x += r2.width;
                r2.x += 10;
                stat.category = EditorGUI.TextField(r2, stat.category);
                
                GUI.enabled = true;

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(ItemManager.database);
                }
            };
        }

        public virtual void Focus()
        {
            //resultList.list = plyAttributes;
        }


        public virtual void Draw()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace(); // Center horizontally
            EditorGUILayout.BeginVertical(EditorStyles.boxStyle, GUILayout.MaxWidth(1000));
            statsScrollPos = EditorGUILayout.BeginScrollView(statsScrollPos);


            // update.., ply changed
            if (plyAttributes.Count != ItemManager.database.plyAttributes.Length)
            {
                var newAtts = attributes;
                var currentAtts = ItemManager.database.plyAttributes;
                for (int j = 0; j < newAtts.Length; j++)
                {
                    if (j < currentAtts.Length - 1)
                    {
                        var c = currentAtts.FirstOrDefault(o => o.ID.Value.ToString() == newAtts[j].ID.Value.ToString());
                        if (c != null)
                        {
                            // Copy from old
                            newAtts[j] = c;
                            //newAtts[j].= c.show;                               
                        }
                    }
                }

                ItemManager.database.plyAttributes = newAtts;
                resultList.list = ItemManager.database.plyAttributes;
            }


            #region UI picker

            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField("Step 1: Pick the ply attributes you wish to show in the UI.", EditorStyles.titleStyle);

            if (resultList.list.Count != plyAttributes.Count)
            {
                // 
            }
            else
            {
                EditorGUILayout.BeginVertical();
                resultList.DoLayoutList();
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndVertical();

            #endregion


            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        public override string ToString()
        {
            return name;
        }
    }
}

#endif