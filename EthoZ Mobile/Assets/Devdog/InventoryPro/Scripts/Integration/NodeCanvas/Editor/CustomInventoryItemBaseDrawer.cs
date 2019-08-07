#if NODE_CANVAS

using System;
using Devdog.General.Editors;
using ParadoxNotion.Design;
using UnityEditor;
using UnityEngine;

namespace Devdog.InventoryPro.Integration.NodeCanvas
{
    public class CustomInventoryItemBaseDrawer : ObjectDrawer<InventoryItemBase>
    {
        private InventoryItemBase _pickedValue;
        public override InventoryItemBase OnGUI(GUIContent label, InventoryItemBase instance)
        {
            Rect rect = GUILayoutUtility.GetRect(300, 300f, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight, GUILayout.ExpandHeight(false));
            rect.x += 5f;
            rect.width -= 5f;

            ObjectPickerUtility.RenderObjectPickerForType<InventoryItemBase>(rect, label.text, instance, (val) =>
            {
                _pickedValue = val;
            });

            if (_pickedValue != null)
            {
                instance = _pickedValue;
                _pickedValue = null;
            }

            return instance;
        }
    }
}

#endif