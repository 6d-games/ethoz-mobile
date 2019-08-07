#if UFPS__

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using Devdog.InventoryPro.Integration.UFPS;

namespace Devdog.InventoryPro.Editors
{
    [CustomEditor(typeof(ConsumableUFPSInventoryItem), true)]
    [CanEditMultipleObjects()]
    public class ConsumableUFPSInventoryItemEditor : InventoryItemBaseEditor
    {

        private ConsumableUFPSInventoryItem tar;


        public override void OnEnable()
        {
            base.OnEnable();


            tar = (ConsumableUFPSInventoryItem)target;

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

            base.OnCustomInspectorGUI(l.ToArray());
        }
    }
}

#endif