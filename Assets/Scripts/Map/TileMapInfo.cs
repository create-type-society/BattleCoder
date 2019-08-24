using BattleCoder.Map;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileMapInfo : MonoBehaviour
{
    [FormerlySerializedAs("tilemap")] [SerializeField] private Tilemap rockTilemap;
    [FormerlySerializedAs("tilemap")] [SerializeField] private Tilemap holeTilemap;
    [SerializeField] private GameObject startPlayerMarker1;
    [SerializeField] private GameObject startPlayerMarker2;

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        Vector3Int tilePos = rockTilemap.WorldToCell(worldPos);
        return new GridPosition(tilePos.x, tilePos.y);
    }

    public TileType GetTileType(Vector3 worldPos)
    {
        Vector3Int tilePos = rockTilemap.WorldToCell(worldPos);
        TileBase rockTile = rockTilemap.GetTile(new Vector3Int(tilePos.x, tilePos.y, 0));
        TileBase holeTile = holeTilemap.GetTile(new Vector3Int(tilePos.x, tilePos.y, 0));
        return SearchTileType(rockTile,holeTile);
    }

    public TileType GetTileType(GridPosition pos)
    {
        TileBase rockTile = rockTilemap.GetTile(new Vector3Int(pos.X, pos.Y, 0));
        TileBase holeTile  = holeTilemap.GetTile(new Vector3Int(pos.X, pos.Y, 0));
        return SearchTileType(rockTile,holeTile);
    }

    public Vector3 GetPlayer1StartPosition()
    {
        return startPlayerMarker1.gameObject.transform.position;
    }

    public Vector3 GetPlayer2StartPosition()
    {
        return startPlayerMarker2.gameObject.transform.position;
    }

    private TileType SearchTileType(TileBase rockTile,TileBase holeTile)
    {
        if (rockTile == null && holeTile == null) return TileType.empty;
        if (rockTile == null)
        {
            if (holeTile.name.Contains("tank")) return TileType.tank;
            if (holeTile.name.Contains("rock")) return TileType.rock;
            if (holeTile.name.Contains("hole")) return TileType.hole;
        }
        else
        {
            if (rockTile.name.Contains("tank")) return TileType.tank;
            if (rockTile.name.Contains("rock")) return TileType.rock;
            if (rockTile.name.Contains("hole")) return TileType.hole;
        }
        
        return TileType.empty;
    }
}