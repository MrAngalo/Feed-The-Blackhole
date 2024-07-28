using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(CustomCollider2D))]
public class ProceduralGrid : MonoBehaviour
{
    // float[] compressedTiles = unchecked(new float[] {
    //     FloatHex(0x00000000), FloatHex(0x11111111), FloatHex(0x22222222), FloatHex(0x33333333),
    //     FloatHex(0x44444444), FloatHex(0x55555555), FloatHex(0x66666666), FloatHex(0x77777777),
    //     FloatHex(0x88888888), FloatHex(0x99999999), FloatHex(0xAAAAAAAA), FloatHex(0xBBBBBBBB),
    //     FloatHex(0xCCCCCCCC), FloatHex(0xDDDDDDDD), FloatHex(0xEEEEEEEE), FloatHex(0xFFFFFFFF),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    //     FloatHex(0x76543210), FloatHex(0xfedcba98), FloatHex(0x76543210), FloatHex(0xfedcba98),
    // });

    // int[] compressedMasks = unchecked(new int[] {
    //     (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF,
    //     (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF,
    //     (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF,
    //     (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF,
    //     (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF,
    //     (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF,
    //     (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF,
    //     (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF, (int) 0xFFFFFFFF,
    // });

    private List<int> boundaries;
    private float[] compressedTiles;
    private int[] compressedMasks;

    private CustomCollider2D col;
    private PhysicsShapeGroup2D shapes;

    private Mesh mesh;
    private Material material;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;

    private Dictionary<TileBase, int> tileIdsLookup;

    public Vector2 pivot = Vector2.zero;
    public float unit = 0.5f;
    public TileBase[] tileIds;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        col = GetComponent<CustomCollider2D>();
        material = GetComponent<MeshRenderer>().material;

        tileIdsLookup = new Dictionary<TileBase, int>();
        for (int i = 0; i < tileIds.Length; i += 1)
        {
            TileBase tile = tileIds[i];
            tileIdsLookup[tile] = i;
        }
    }

    // void Start()
    // {
    //     GenerateBoundaries();
    //     CreateMesh();
    //     CreateCollider();
    //     UpdateMesh();
    //     UpdateMaterial();
    //     UpdateCollider();
    // }

    // A list of 32 x 32 tiles
    public void CreateTilesAndMask(TileBase[] tiles)
    {
        compressedTiles = new float[128];
        compressedMasks = new int[32];

        int k = 0;
        int l = 0;
        for (int i = 0; i < tiles.Length; i += 32)
        {
            int m = 0;
            for (int j = 0; j < 32; j += 8)
            {
                GetTileIdAndMask(tiles[i + j], out int t0, out int m0);
                GetTileIdAndMask(tiles[i + j + 1], out int t1, out int m1);
                GetTileIdAndMask(tiles[i + j + 2], out int t2, out int m2);
                GetTileIdAndMask(tiles[i + j + 3], out int t3, out int m3);
                GetTileIdAndMask(tiles[i + j + 4], out int t4, out int m4);
                GetTileIdAndMask(tiles[i + j + 5], out int t5, out int m5);
                GetTileIdAndMask(tiles[i + j + 6], out int t6, out int m6);
                GetTileIdAndMask(tiles[i + j + 7], out int t7, out int m7);

                int t = (t7 << 28) + (t6 << 24) + (t5 << 20) + (t4 << 16)
                      + (t3 << 12) + (t2 << 8) + (t1 << 4) + t0;

                m += (m7 << (j + 7)) + (m6 << (j + 6)) + (m5 << (j + 5)) + (m4 << (j + 4))
                   + (m3 << (j + 3)) + (m2 << (j + 2)) + (m1 << (j + 1)) + (m0 << j);

                compressedTiles[l] = FloatHex(t);
                l += 1;
            }
            compressedMasks[k] = m;
            k += 1;
        }
    }

    void GenerateBoundaries()
    {
        boundaries = new List<int>();
        int[] layers = (int[])this.compressedMasks.Clone();

        for (int y = 0; y < layers.Length; y++)
        {
            int layer = layers[y];
            while (layer != 0)
            {
                int x = TrailingZeroCount(layer);
                // Performs ~(layer >> x) with 0 fill
                int w = TrailingZeroCount((int)~((uint)layer >> x));
                int h = 1;
                // Performs (-1 + (1 << w)) << x without overflowing;
                int mask = (-1 + (1 << w - 1) + (1 << w - 1)) << x;
                layers[y] -= mask;
                while (y + h < layers.Length && (layers[y + h] & mask) == mask)
                {
                    layers[y + h] -= mask;
                    h++;
                }
                boundaries.Add((x << 24) + (y << 16) + (w << 8) + h);
                layer -= mask;
            }
        }
    }

    void CreateMesh()
    {
        vertices = new Vector3[boundaries.Count * 4];
        triangles = new int[boundaries.Count * 6];
        uv = new Vector2[boundaries.Count * 4];

        Vector3 o = new(-pivot.x * 32, -pivot.y * 32, 0);

        int i = 0;
        int j = 0;
        foreach (int b in boundaries)
        {
            byte x = (byte)((b >> 24) & 0xFF);
            byte y = (byte)((b >> 16) & 0xFF);
            byte w = (byte)((b >> 8) & 0xFF);
            byte h = (byte)(b & 0xFF);

            float x1 = unit * (o.x + x);
            float y1 = unit * (o.y + y);
            float x2 = unit * (o.x + x + w);
            float y2 = unit * (o.y + y + h);

            vertices[i] = new(x1, y1, 0);
            vertices[i + 1] = new(x1, y2, 0);
            vertices[i + 2] = new(x2, y1, 0);
            vertices[i + 3] = new(x2, y2, 0);

            triangles[j] = i;
            triangles[j + 1] = i + 1;
            triangles[j + 2] = i + 2;
            triangles[j + 3] = i + 2;
            triangles[j + 4] = i + 1;
            triangles[j + 5] = i + 3;

            uv[i] = new(x, y);
            uv[i + 1] = new(x, y + h);
            uv[i + 2] = new(x + w, y);
            uv[i + 3] = new(x + w, y + h);

            i += 4;
            j += 6;
        }
    }

    void CreateCollider()
    {
        shapes = new PhysicsShapeGroup2D();
        Vector3 position = transform.position;
        for (int i = 0; i < vertices.Length; i += 4)
        {
            List<Vector2> shape = new(4)
            {
                position + vertices[i],
                position + vertices[i + 1],
                position + vertices[i + 3],
                position + vertices[i + 2]
            };
            shapes.AddPolygon(shape);
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }

    void UpdateCollider()
    {
        col.ClearCustomShapes();
        col.SetCustomShapes(shapes);
    }

    void UpdateMaterial()
    {
        material.SetFloatArray("_Tiles", compressedTiles);
    }

    public void CreateAll()
    {
        GenerateBoundaries();
        CreateMesh();
        CreateCollider();
    }

    public void UpdateAll()
    {
        UpdateMesh();
        UpdateMaterial();
        UpdateCollider();
    }

    void GetTileIdAndMask(TileBase tile, out int id, out int mask)
    {
        if (tile != null && tileIdsLookup.TryGetValue(tile, out id))
        {
            id &= 0xF;
            mask = 1;
        }
        else
        {
            id = 0;
            mask = 0;
        }
    }

    // Modified from https://stackoverflow.com/questions/10439242/count-leading-zeroes-in-an-int32
    static int TrailingZeroCount(int x)
    {
        // Do the smearing
        x |= x << 1;
        x |= x << 2;
        x |= x << 4;
        x |= x << 8;
        x |= x << 16;
        // Count the ones
        x -= x >> 1 & 0x55555555;
        x = (x >> 2 & 0x33333333) + (x & 0x33333333);
        x = (x >> 4) + x & 0x0f0f0f0f;
        x += x >> 8;
        x += x >> 16;
        return 32 - (x & 0x0000003f); // Subtract # of 1s from 32
    }

    static float FloatHex(int intValue)
    {
        byte[] intBytes = BitConverter.GetBytes(intValue);
        return BitConverter.ToSingle(intBytes, 0);
    }
}
