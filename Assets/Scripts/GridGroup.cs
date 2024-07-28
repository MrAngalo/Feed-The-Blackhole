using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGroup : MonoBehaviour
{
    private Tilemap tilemap;

    private Vector2 offset;
    private Dictionary<TileBase, int> tileIdsLookup;

    public Material material;
    public TileBase[] tileIds;

    void Awake()
    {
        tileIdsLookup = new Dictionary<TileBase, int>();
        for (int i = 0; i < tileIds.Length; i += 1)
        {
            TileBase tile = tileIds[i];
            tileIdsLookup[tile] = i;
        }
    }

    void Start()
    {
        CreateGridGroup();
        CleanUp();
    }

    void CreateGridGroup()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        offset = new Vector2(bounds.xMin, bounds.yMin);
        
        for (int x = bounds.xMin; x < bounds.xMax; x += 32)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y += 32)
            {
                BoundsInt sub = new BoundsInt(x, y, 0, 32, 32, 1);
                TileBase[] tiles = tilemap.GetTilesBlock(sub);
                CreateProceduralGrid(x, y, tiles);
            }
        }
    }

    void CreateProceduralGrid(int x, int y, TileBase[] tiles)
    {
        if (ProceduralGrid.Create(tileIdsLookup, tiles, material, out ProceduralGrid grid))
        {
            grid.transform.parent = transform;
            grid.transform.position = tilemap.transform.TransformVector(x, y, 0);
            grid.transform.localScale = Vector3.one;
            grid.CreateAll();
            grid.UpdateAll();
        }
    }

    void CleanUp()
    {
        Destroy(tilemap.gameObject);
        Destroy(GetComponent<Grid>());
    }
}
