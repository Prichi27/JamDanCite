using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MeshParticleSystem : MonoBehaviour
{
    private const int MAX_QUAD_AMOUNT = 15000;

    [Serializable]
    public struct ParticleUVPixels
    {
        public Vector2Int uv00Pixel;
        public Vector2Int uv11Pixel;
    }

    private struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    private Mesh mesh;

    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;

    [SerializeField] private ParticleUVPixels[] particleUVPixels;

    private UVCoords[] UVCoordsArray;

    private int quadIndex;

    private bool updateVertices;
    private bool updateUV;
    private bool updateTriangles;
    private void Awake()
    {
        mesh = new Mesh();

        vertices = new Vector3[4 * MAX_QUAD_AMOUNT];
        uv = new Vector2[4 * MAX_QUAD_AMOUNT];
        triangles = new int[6 * MAX_QUAD_AMOUNT];

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        
        GetComponent<MeshFilter>().mesh = mesh;

        Material mat = GetComponent<MeshRenderer>().material;
        Texture texture = mat.mainTexture;
        int textureWidth = texture.width;
        int textureHeight = texture.height;
        List<UVCoords> uvCoordList = new List<UVCoords>();
        foreach (ParticleUVPixels pixels in particleUVPixels)
        {
            UVCoords uvCoords = new UVCoords
            {
                uv00 = new Vector2((float)pixels.uv00Pixel.x / textureWidth, (float)pixels.uv00Pixel.y / textureHeight),
                uv11 = new Vector2((float)pixels.uv11Pixel.x / textureWidth, (float)pixels.uv11Pixel.y / textureHeight)
            };

            uvCoordList.Add(uvCoords);
        }

        UVCoordsArray = uvCoordList.ToArray();
    }

    public int AddQuad(Vector3 pos, float rot, Vector3 scale, bool skewed, int uvIndex)
    {
        if (quadIndex >= MAX_QUAD_AMOUNT) return 0;

        UpdateQuad(quadIndex, pos, rot, scale, skewed, uvIndex);

        int spawnedIndex = quadIndex;

        quadIndex++;

        return spawnedIndex;
    }

    public void UpdateQuad(int i, Vector3 pos, float rot, Vector3 scale, bool skewed, int uvIndex)
    {
        //Relocate vertices
        int vIndex = i * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        if (skewed)
        {
            vertices[vIndex0] = pos + Quaternion.Euler(0, 0, rot) * new Vector3(-scale.x, -scale.y);
            vertices[vIndex1] = pos + Quaternion.Euler(0, 0, rot) * new Vector3(-scale.x, +scale.y);
            vertices[vIndex2] = pos + Quaternion.Euler(0, 0, rot) * new Vector3(+scale.x, +scale.y);
            vertices[vIndex3] = pos + Quaternion.Euler(0, 0, rot) * new Vector3(+scale.x, -scale.y);
        }

        else
        {
            vertices[vIndex0] = pos + Quaternion.Euler(0, 0, rot - 180) * scale;
            vertices[vIndex1] = pos + Quaternion.Euler(0, 0, rot - 270) * scale;
            vertices[vIndex2] = pos + Quaternion.Euler(0, 0, rot - 0) * scale;
            vertices[vIndex3] = pos + Quaternion.Euler(0, 0, rot - 90) * scale;
        }

        // UV
        UVCoords uVCoords = UVCoordsArray[uvIndex];
        uv[vIndex0] = uVCoords.uv00;
        uv[vIndex1] = new Vector2(uVCoords.uv00.x, uVCoords.uv11.y);
        uv[vIndex2] = uVCoords.uv11;
        uv[vIndex3] = new Vector2(uVCoords.uv11.x, uVCoords.uv00.y);

        //Create triangles
        int tIndex = i * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        updateVertices = true;
        updateUV = true;
        updateTriangles = true;

    }

    public void DestroyQuad(int i)
    {
        int vIndex = i * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        vertices[vIndex0] = Vector3.zero;
        vertices[vIndex1] = Vector3.zero; 
        vertices[vIndex2] = Vector3.zero; 
        vertices[vIndex3] = Vector3.zero;

        updateVertices = true;
    }

    private void LateUpdate()
    {
        if(updateVertices)
        {
            mesh.vertices = vertices;
            updateVertices = false;
        }

        if (updateUV)
        {
            mesh.uv = uv;
            updateUV = false;
        }

        if (updateTriangles)
        {
            mesh.triangles = triangles;
            updateTriangles = false;
        }

        if (updateVertices || updateUV || updateTriangles)
        {
            GetComponent<MeshFilter>().mesh = mesh;
        }
        mesh.RecalculateBounds();
    }
}
