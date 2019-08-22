using BattleCoder.Map;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapInfo : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject startPlayerMarker1;
    [SerializeField] private GameObject startPlayerMarker2;

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);
        return new GridPosition(tilePos.x, tilePos.y);
    }

    public TileType GetTileType(Vector3 worldPos)
    {
        Vector3Int tilePos = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(new Vector3Int(tilePos.x, tilePos.y, 0));
        return SearchTileType(tile);
    }

    public TileType GetTileType(GridPosition pos)
    {
        TileBase tile = tilemap.GetTile(new Vector3Int(pos.X, pos.Y, 0));
        return SearchTileType(tile);
    }

    public Vector3 GetPlayer1StartPosition()
    {
        return startPlayerMarker1.gameObject.transform.position;
    }

    public Vector3 GetPlayer2StartPosition()
    {
        return startPlayerMarker2.gameObject.transform.position;
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