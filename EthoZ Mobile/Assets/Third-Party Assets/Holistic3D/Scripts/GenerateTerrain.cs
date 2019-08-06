using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {
    int heightScale = 5;
    float detailScale = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] verticies = mesh.vertices;
        for(int v = 0; v < verticies.Length; v++)
        {
            verticies[v].y = Mathf.PerlinNoise((verticies[v].x + this.transform.position.x)/detailScale,
                                                (verticies[v].z + this.transform.position.z)/detailScale)*heightScale;
        }

        mesh.vertices = verticies;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
