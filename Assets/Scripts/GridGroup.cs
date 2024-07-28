using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGroup : MonoBehaviour
{
    private Tilemap tilemap;
    private Dictionary<TileBase, int> tileIdsLookup;

    private Vector2 offset;
    private int worldWidth;
    private int worldHeight;
    private int gridWidth;
    private int gridHeight;
    private int[] worldDataIndex;
    private List<ProceduralGrid> worldData;

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
        CreateProceduralGrid();
        CleanUp();
    }

    void CreateProceduralGrid()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        offset = new Vector2(bounds.xMin, bounds.yMin);

        gridWidth = (bounds.size.x >> 5) + 1;
        gridHeight = (bounds.size.y >> 5) + 1;

        worldWidth = gridWidth << 5;
        worldHeight = gridHeight << 5;
        worldDataIndex = new int[gridWidth * gridHeight];
        worldData = new();

        int i = 0;
        for (int x = bounds.xMin; x < bounds.xMax; x += 32)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y += 32)
            {
                BoundsInt sub = new(x, y, 0, 32, 32, 1);
                TileBase[] tiles = tilemap.GetTilesBlock(sub);
                if (ProceduralGrid.Create(tileIdsLookup, tiles, material, out ProceduralGrid grid))
                {
                    grid.transform.parent = transform;
                    grid.transform.position = tilemap.transform.TransformVector(x, y, 0);
                    grid.transform.localScale = Vector3.one;
                    grid.CreateAll();
                    grid.UpdateAll();

                    worldDataIndex[i] = worldData.Count;
                    worldData.Add(grid);
                }
                else
                {
                    worldDataIndex[i] = -1;
                }
                i++;
            }
        }
    }

    void CleanUp()
    {
        Destroy(tilemap.gameObject);
        Destroy(GetComponent<Grid>());
    }

    void BreakBlock(int x, int y) {
        if (x < 0 || y < 0 || x >= worldWidth || y >= worldHeight)
        {
            return;
        }
        int index = (y * gridWidth + x) >> 5;
        if (index == -1)
        {
            return;
        }
        int localX = x & 0x1F;
        int localY = y & 0x1F;
        ProceduralGrid grid = worldData[worldDataIndex[index]];
        grid.BreakBlock(localX, localY);
    }
}
