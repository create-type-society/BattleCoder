using BattleCoder.Map;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapInfo : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    private TileBase tile;
    private GridPosition pos;
    
    public TileType GetTileType(GridPosition pos)
    {
        tile = tilemap.GetTile(new Vector3Int(pos.X,pos.Y, 0));
        return SearchTileType(tile);
    }

    private TileType SearchTileType(TileBase tile)
    {
        switch (tile.name)
        {
            case @"[1-9]*_tanc":
                return TileType.tanc;
            case @"[1-9]*_rock":
                return TileType.rock;
            case @"[1-9]*_hole":
                return TileType.hole;
            default:
                return TileType.empty;
        }
    }
}