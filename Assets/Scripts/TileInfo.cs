using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public struct Tiles
{
    public int Tanc;
    public int Rock;
    public int Hole;
    public int Empty;
}

public class TileInfo : MonoBehaviour
{
    private Tilemap tilemap = new Tilemap();
    private TileBase tiles;

    public TileInfo(Tilemap tilemap)
    {
        this.tilemap = tilemap;
    }

    public void GetTile(int x, int y)
    {
        tiles = tilemap.GetTile(new Vector3Int(x, y, 0));
        tilemap.SetTile(new Vector3Int(0, 0, 0), null);
        Debug.Log(tiles);
    }
}