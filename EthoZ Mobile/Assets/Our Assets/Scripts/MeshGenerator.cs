using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    public int xSize = 6000;
    public int zSize = 6000;

    public float xScale = 1f;
    public float yScale = 1f;
    public float zScale = 1f;

    public float offsetX = 100;
    public float offsetZ = 100;

    public Gradient gradient;

    float minTerrainHeight;
    float maxTerrainHeight;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetZ = Random.Range(0f, 9999f);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        
        for(int i = 0, z = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f + offsetX, z * .3f + offsetZ) * 2f;
                vertices[i] = new Vector3(x, y, z);

                if(y > maxTerrainHeight)
                {
                    maxTerrainHeight = y;
                }

                if(y < minTerrainHeight)
                {
                    minTerrainHeight = y;
                }

                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for(int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

       colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();

        this.gameObject.transform.localScale = new Vector3(xScale, yScale, zScale);

        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
