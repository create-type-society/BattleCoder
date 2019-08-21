using System.Text.RegularExpressions;
using BattleCoder.Map;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapInfo : MonoBehaviour
{
    private Tilemap tilemap;
    private TileBase tile;
    private GridPosition pos;
    
    public TileType GetTileType(GridPosition pos,Tilemap tilemap)
    {
        this.tilemap = tilemap;
        tile = tilemap.GetTile(new Vector3Int(pos.X,pos.Y, 0));
        return SearchTileType(tile);
    }

    private TileType SearchTileType(TileBase tile)
    {
        if (tile == null) return TileType.empty;
        if (tile.name.Contains("tanc")) return TileType.tanc;
        if (tile.name.Contains("rock")) return TileType.rock;
        if (tile.name.Contains("hole")) return TileType.hole;
        return TileType.empty;
    }
}