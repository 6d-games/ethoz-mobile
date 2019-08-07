#if UFPS

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using Devdog.InventoryPro.Integration.UFPS;
using Devdog.InventoryPro;

namespace Devdog.InventoryPro.Editors
{
    [CustomEditor(typeof(UnitTypeUFPSInventoryItem), true)]
    [CanEditMultipleObjects()]
    public class UnitTypeUFPSInventoryItemEditor : EquippableInventoryItemEditor
    {

        private UnitTypeUFPSInventoryItem tar;


        public override void OnEnable()
        {
            base.OnEnable();

            tar = (UnitTypeUFPSInventoryItem)target;
        }

        protected override void OnCustomInspectorGUI(params CustomOverrideProperty[] extraOverride)
        {
            var l = new List<CustomOverrideProperty>(extraOverride);
            if (tar.useUFPSItemData)
            {
                l.Add(new CustomOverrideProperty("_id", null));
                l.Add(new CustomOverrideProperty("_name", null));
                l.Add(new CustomOverrideProperty("_description", null));
            }

            var nameBefore = tar.name;
            base.OnCustomInspectorGUI(l.ToArray());

            // Changed
            if (tar.name != nameBefore)
            {
                // Update name
                ItemEditor.UpdateAssetName(tar);

                if (tar.unitType != null && tar.useUFPSItemData)
                {
                    tar.name = string.Empty;
                    tar.description = string.Empty;
                }
            }
        }
    }
}

#endif