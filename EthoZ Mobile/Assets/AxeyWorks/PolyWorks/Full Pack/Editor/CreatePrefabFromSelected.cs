using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using Object = UnityEngine.Object;

class CreatePrefabFromSelected : ScriptableObject
{
    const string menuTitle = "GameObject/Create Prefab From Selected";

    [MenuItem(menuTitle)]
    static void CreatePrefab()
    {
        var objs = Selection.gameObjects;

        string pathBase = EditorUtility.SaveFolderPanel("Choose save folder", "Assets", "");

        if (!String.IsNullOrEmpty(pathBase))
        {

            pathBase = pathBase.Remove(0, pathBase.IndexOf("Assets")) + Path.DirectorySeparatorChar;

            foreach (var go in objs)
            {
                String localPath = pathBase + go.name + ".prefab";

                localPath = localPath.Replace('\\', '/');

                if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
                {
                    if (EditorUtility.DisplayDialog("Are you sure?",
                        "The prefab already exists. Do you want to overwrite it?",
                        "Yes",
                        "No"))
                        CreateNew(go, localPath);
                }
                else
                    CreateNew(go, localPath);
            }
        }
    }

    static void CreateNew(GameObject obj, string localPath)
    {
        localPath = localPath.Replace('\\', '/');
        Object prefab = PrefabUtility.CreatePrefab(localPath, obj);
        PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
        AssetDatabase.Refresh();
    }

    [MenuItem(menuTitle, true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null;
    }
}