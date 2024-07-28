using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    private Tilemap tilemap;

    public GameObject gridPrefab;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        Debug.Log(bounds);
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
        Vector3 position = new Vector3(x, y, 0);
        ProceduralGrid grid = Instantiate(gridPrefab).GetComponent<ProceduralGrid>();
        grid.transform.parent = transform.parent;
        grid.transform.localPosition = position;
        grid.transform.localScale = Vector3.one;
        grid.CreateTilesAndMask(tiles);
        grid.CreateAll();
        grid.UpdateAll();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
