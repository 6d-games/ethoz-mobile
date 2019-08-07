#if PLY_GAME

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Devdog.General.Editors;
using plyGame;
using UnityEditor;
using UnityEngine;

namespace Devdog.InventoryPro.Editors
{
    [CustomObjectPicker(typeof(plyGame.Skill))]
    public class InventoryPlySkillPrefabPickerBase : ObjectPickerBaseEditor
    {
        
    }
}

#endif