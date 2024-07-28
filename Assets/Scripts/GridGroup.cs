using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGroup : MonoBehaviour
{
    public GameObject gridPrefab;

    void Start()
    {
        CreateGridGroup();
        CleanUp();
    }

    void CreateGridGroup() {
        Tilemap tilemap = GetComponentInChildren<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x+= 32) {
            for (int y = bounds.yMin; y < bounds.yMax; y+= 32) {
                BoundsInt sub = new BoundsInt(x, y, 0, 32, 32, 1);
                TileBase[] tiles = tilemap.GetTilesBlock(sub);
                CreateProceduralGrid(x, y, tiles);
            }
        }
    }

    void CreateProceduralGrid(int x, int y, TileBase[] tiles)
    {
        Vector3 position = new(x, y, 0);
        ProceduralGrid grid = Instantiate(gridPrefab).GetComponent<ProceduralGrid>();
        grid.transform.parent = transform;
        grid.transform.localPosition = position;
        grid.transform.localScale = Vector3.one;
        grid.CreateTilesAndMask(tiles);
        grid.CreateAll();
        grid.UpdateAll();
    }

    void CleanUp() {
        Tilemap tilemap = GetComponentInChildren<Tilemap>();
        Destroy(tilemap.gameObject);
        Destroy(GetComponent<Grid>());
    }
}
