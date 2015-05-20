using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class CircleTerrain : MonoBehaviour {

    public float variation;
    public float resolution;

    public float radius;

    public float height;

    private TerrainPiece data;

    void Start()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        data = new TerrainPiece(transform.position, radius, variation, Random.seed, resolution);
        GenerateMesh();
    }



    private void GenerateMesh()
    {
        Vector3[] verticies = new Vector3[data.Verticies.Length + 1];

        verticies[0] = new Vector3(0, 0, 0);

        for (int i = 1; i < verticies.Length; i++)
        {
            verticies[i] = new Vector3(data.Verticies[i-1].x, data.Verticies[i-1].y, 0);
        }

        int[] triangles = new int[verticies.Length];

        int size = triangles.Length;
        Debug.Log(size);
        for (int j = 0; j < triangles.Length; j+=3)
        {
            if (j != triangles.Length - 2)
            {
                triangles[j] = 0;
                triangles[j + 1] = j + 2;
                triangles[j + 2] = Mathf.Abs((j - 1)%size);
            }
        }

        Mesh m = new Mesh();
        m.vertices = verticies;
        m.triangles = triangles;

        MeshFilter mf = GetComponent<MeshFilter>();

        mf.mesh = m;
    }
}
