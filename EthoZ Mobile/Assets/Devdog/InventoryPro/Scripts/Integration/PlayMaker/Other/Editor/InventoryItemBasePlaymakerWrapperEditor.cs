#if PLAYMAKER

using System;
using System.Collections.Generic;
using Devdog.General.Editors;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using EditorStyles = UnityEditor.EditorStyles;
using PropertyDrawer = HutongGames.PlayMakerEditor.PropertyDrawer;

namespace Devdog.InventoryPro.Integration.PlayMaker
{
    [PropertyDrawer(typeof(InventoryItemPlayerMakerAdapter))]
    public class InventoryItemBasePlaymakerWrapperEditor : PropertyDrawer
    {
        private InventoryItemBase _item;

        public override object OnGUI(GUIContent label, object obj, bool isSceneObject, params object[] attributes)
        {
            var t = (InventoryItemPlayerMakerAdapter)obj;
            if (t.item == null)
            {
                GUI.color = Color.yellow;
            }

            ObjectPickerUtility.RenderObjectPickerForType<InventoryItemBase>(label.text, t.item, item =>
            {
                _item = item;
                GUI.changed = true;
            });

            if (_item != null)
            {
                t.item = _item;
                _item = null;
                GUI.changed = true;
            }

            GUI.color = Color.white;
            return t;
        }
    }
}

#endif