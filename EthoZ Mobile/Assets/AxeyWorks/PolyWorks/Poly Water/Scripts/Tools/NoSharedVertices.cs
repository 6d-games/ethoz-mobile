using UnityEngine;
using UnityEditor;

//public class NoSharedVertices : EditorWindow
//{
//
//    private string error = "";
//
//    [MenuItem("Window/No Shared Vertices")]
//    public static void ShowWindow()
//    {
//        EditorWindow.GetWindow(typeof(NoSharedVertices));
//    }
//
//    void OnGUI()
//    {
//        //Transform curr = Selection.activeTransform;
//        GUILayout.Label("Creates a clone of the game object where the triangles\n" +
//            "do not share vertices");
//        GUILayout.Space(20);
//
//        if (GUILayout.Button("Process"))
//        {
//            error = "";
//            NoShared();
//        }
//
//        GUILayout.Space(20);
//        GUILayout.Label(error);
//    }
//
//    void NoShared()
//    {
//        var curr = Selection.activeTransform;
//
//        if (curr == null)
//        {
//            error = "No appropriate object selected.";
//            Debug.Log(error);
//            return;
//        }
//
//        MeshFilter mf;
//        mf = curr.GetComponent<MeshFilter>();
//        if (mf == null || mf.sharedMesh == null)
//        {
//            error = "No mesh on the selected object";
//            Debug.Log(error);
//            return;
//        }
//
//        // Create the duplicate game object
//        var go = Instantiate(curr.gameObject) as GameObject;
//        mf = go.GetComponent<MeshFilter>();
//        var mesh = Instantiate(mf.sharedMesh) as Mesh;
//        mf.sharedMesh = mesh;
//        Selection.activeObject = go.transform;
//
//        //Process the triangles
//        var oldVerts = mesh.vertices;
//        var triangles = mesh.triangles;
//        var vertices = new Vector3[triangles.Length];
//        for (var i = 0; i < triangles.Length; i++)
//        {
//            vertices[i] = oldVerts[triangles[i]];
//            triangles[i] = i;
//        }
//        mesh.vertices = vertices;
//        mesh.triangles = triangles;
//        mesh.RecalculateBounds();
//        mesh.RecalculateNormals();
//
//        // Save a copy to disk
//        var name = "Assets/" + go.name + Random.Range(0, int.MaxValue).ToString() + ".asset";
//        AssetDatabase.CreateAsset(mf.sharedMesh, name);
//        AssetDatabase.SaveAssets();
//    }
//}