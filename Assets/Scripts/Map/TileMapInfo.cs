using System.Text.RegularExpressions;
using BattleCoder.Map;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapInfo : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    

    
    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);
        return new GridPosition(tilePos.x, tilePos.y);
    }
    
    public TileType GetTileType(Vector3 worldPos)
    {
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(new Vector3Int(tilePos.x,tilePos.y, 0));
        return SearchTileType(tile);
    }
    public TileType GetTileType(GridPosition pos)
    {
        TileBase　tile = tilemap.GetTile(new Vector3Int(pos.X,pos.Y, 0));
        return SearchTileType(tile);
    }

    private TileType SearchTileType(TileBase tile)
    {
        if (tile == null) return TileType.empty;
        if (tile.name.Contains("tank")) return TileType.tank;
        if (tile.name.Contains("rock")) return TileType.rock;
        if (tile.name.Contains("hole")) return TileType.hole;
        return TileType.empty;
    }
}