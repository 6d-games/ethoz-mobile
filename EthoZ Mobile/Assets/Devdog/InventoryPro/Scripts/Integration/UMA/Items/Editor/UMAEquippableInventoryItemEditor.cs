#if UMA

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using Devdog.General.Editors;
using Devdog.InventoryPro.Integration.UMA;
using Devdog.InventoryPro;
using UnityEditorInternal;
using EditorUtility = UnityEditor.EditorUtility;

namespace Devdog.InventoryPro.Editors
{
    [CustomEditor(typeof(UMAEquippableInventoryItem), true)]
    public class UMAEquippableInventoryItemEditor : InventoryItemBaseEditor
    {
        protected SerializedProperty equipType;

        private SerializedProperty equipSlotsData;



        private ReorderableList reorderableList;
        
        
        


        public override void OnEnable()
        {
            base.OnEnable();

            equipType = serializedObject.FindProperty("_equipmentType");
            equipSlotsData = serializedObject.FindProperty("equipSlotsData");

            reorderableList = new ReorderableList(serializedObject, equipSlotsData, false, true, true, true);
            reorderableList.drawHeaderCallback += rect => EditorGUI.LabelField(rect, "UMA Equipment data");
            reorderableList.elementHeight = 210;
            reorderableList.drawElementCallback += (rect, index, active, focused) =>
            {
//                var t = (UMAEquippableInventoryItem)target;

                rect.height = 16;
                rect.y += 2;
                
                var o = equipSlotsData.GetArrayElementAtIndex(index);
                var umaEquipSlot = o.FindPropertyRelative("umaEquipSlot");
                var umaOverrideColor = o.FindPropertyRelative("umaOverrideColor");
                var umaOverlayColor = o.FindPropertyRelative("umaOverlayColor");
                var umaOverlayDataAsset = o.FindPropertyRelative("umaOverlayDataAsset");
                var umaSlotDataAsset = o.FindPropertyRelative("umaSlotDataAsset");
                var umaReplaceSlot = o.FindPropertyRelative("umaReplaceSlot");

                EditorGUI.LabelField(rect, "Equipment item " + index, UnityEditor.EditorStyles.boldLabel);
                rect.y += 20;

                EditorGUI.PropertyField(rect, umaEquipSlot);
                rect.y += 18;


                rect.y += 5;
                EditorGUI.PropertyField(rect, umaOverrideColor);
                rect.y += 18;

                if (umaOverrideColor.boolValue)
                {
                    EditorGUI.PropertyField(rect, umaOverlayColor);
                    rect.y += 18;
                }
                rect.y += 5;

                EditorGUI.PropertyField(rect, umaOverlayDataAsset);
                rect.y += 18;

                EditorGUI.PropertyField(rect, umaSlotDataAsset);
                rect.y += 18;

                EditorGUI.PropertyField(rect, umaReplaceSlot);
                rect.y += 23;

                string msg = "";
                rect.height = 40;
                if (umaEquipSlot.objectReferenceValue != null)
                {
                    if (umaOverlayDataAsset.objectReferenceValue != null && umaSlotDataAsset.objectReferenceValue == null && umaReplaceSlot.objectReferenceValue == null)
                    {
                        msg = "Equipping " + umaOverlayDataAsset.objectReferenceValue.name + "\nto\n" + umaEquipSlot.objectReferenceValue.name;
                    }
                    else if (umaOverlayDataAsset.objectReferenceValue != null && umaSlotDataAsset.objectReferenceValue != null && umaReplaceSlot.objectReferenceValue == null)
                    {
                        msg = "Equipping " + umaOverlayDataAsset.objectReferenceValue.name + " & " + umaSlotDataAsset.objectReferenceValue.name + "\nto\n" + umaEquipSlot.objectReferenceValue.name;
                    }
                    else if (umaOverlayDataAsset.objectReferenceValue != null && umaSlotDataAsset.objectReferenceValue != null && umaReplaceSlot.objectReferenceValue != null)
                    {
                        rect.height += 14;
                        msg = "Equipping " + umaOverlayDataAsset.objectReferenceValue.name + " & " + umaSlotDataAsset.objectReferenceValue.name + "\nto\n" + umaEquipSlot.objectReferenceValue.name + "\nReplacing slot " + umaReplaceSlot.objectReferenceValue.ToString() + " while equipped.";
                    }
                }

                if(msg != "")
                    EditorGUI.HelpBox(rect, msg, MessageType.Info);



                // Changed something, copy property data
                if (GUI.changed)
                {
                    EditorUtility.SetDirty(target);
                    serializedObject.ApplyModifiedProperties();
                    Repaint();
                }
            };
        }

        protected override void DrawItemStatLookup(Rect rect, SerializedProperty property, bool isactive, bool isfocused, bool drawRestore, bool drawPercentage)
        {
            base.DrawItemStatLookup(rect, property, isactive, isfocused, false, drawPercentage);
        }

        protected override void OnCustomInspectorGUI(params CustomOverrideProperty[] extraOverride)
        {
            var l = new List<CustomOverrideProperty>(extraOverride);
            l.Add(new CustomOverrideProperty("_equipmentType", () =>
            {

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("EquippedItem type", GUILayout.Width(EditorGUIUtility.labelWidth - 5));

                EditorGUILayout.BeginVertical();
                EditorGUILayout.HelpBox("Edit types in the EquippedItem editor", MessageType.Info);
                ObjectPickerUtility.RenderObjectPickerForType<EquipmentType>(string.Empty, equipType);
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();
            }));
            l.Add(new CustomOverrideProperty("equipVisually", null));
            l.Add(new CustomOverrideProperty("equipPosition", null));
            l.Add(new CustomOverrideProperty("equipRotation", null));


            l.Add(new CustomOverrideProperty(equipSlotsData.name, () =>
                {                   
                    reorderableList.DoLayoutList();
                }));

//            l.Add(new CustomOverrideProperty(umaEquipSlot.name, () =>
//                {
//                    EditorGUILayout.PropertyField(umaEquipSlot);
//                    EditorGUILayout.Space();
//                }));
//            l.Add(new CustomOverrideProperty(umaOverrideColor.name, () => EditorGUILayout.PropertyField(umaOverrideColor)));
//            l.Add(new CustomOverrideProperty(umaOverlayColor.name, () =>
//                {
//                    if (umaOverrideColor.boolValue)
//                    {
//                        EditorGUILayout.PropertyField(umaOverlayColor);
//                    }
//
//                    EditorGUILayout.Space();
//                }));
//            l.Add(new CustomOverrideProperty(umaOverlayDataAsset.name, () => EditorGUILayout.PropertyField(umaOverlayDataAsset)));
//            l.Add(new CustomOverrideProperty(umaSlotDataAsset.name, () => EditorGUILayout.PropertyField(umaSlotDataAsset)));
//            l.Add(new CustomOverrideProperty(umaReplaceSlot.name, () =>
//                {
//                    EditorGUILayout.PropertyField(umaReplaceSlot);
//
//                    string msg = "";
//                    if (umaOverlayDataAsset.objectReferenceValue != null && umaSlotDataAsset.objectReferenceValue == null && umaReplaceSlot.objectReferenceValue == null)
//                    {
//                        msg = "Equipping UMA object to equip slot";
//                    }
//                    else if (umaOverlayDataAsset.objectReferenceValue != null && umaSlotDataAsset.objectReferenceValue != null && umaReplaceSlot.objectReferenceValue == null)
//                    {
//                        msg = "Equipping UMA object to a new slot that will be created at run-time if it doesn't exist yet.";
//                    }
//                    else if (umaOverlayDataAsset.objectReferenceValue != null && umaSlotDataAsset.objectReferenceValue != null && umaReplaceSlot.objectReferenceValue != null)
//                    {
//                        msg = "Equipping UMA object to a new slot that will be created at run-time if it doesn't exist yet.\nReplacing slot " + umaReplaceSlot.objectReferenceValue.ToString() + " while equipped.";
//                    }
//
//                    EditorGUILayout.HelpBox(msg, MessageType.Info);
//                }));
//

            


            base.OnCustomInspectorGUI(l.ToArray());
        }
    }
}

#endif