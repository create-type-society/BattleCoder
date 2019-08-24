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

    public Transform EnemyTankTransform { private get; set; }

    public GridPosition GetGridPosition(Vector3 worldPos)
    {
        Vector3Int tilePos = rockTilemap.WorldToCell(worldPos);
        return new GridPosition(tilePos.x, tilePos.y);
    }
    
    public TileType GetTileType(Vector3 worldPos)
    {
        GridPosition tilePos = GetGridPosition(worldPos);
        TileBase rockTile = rockTilemap.GetTile(new Vector3Int(tilePos.X, tilePos.Y, 0));
        TileBase holeTile = holeTilemap.GetTile(new Vector3Int(tilePos.X, tilePos.Y, 0));
        GridPosition enemyPos = GetGridPosition(EnemyTankTransform.position);
        return SearchTileType(rockTile,holeTile,enemyPos,tilePos);
    }

    public TileType GetTileType(GridPosition tilePos)
    {
        TileBase rockTile = rockTilemap.GetTile(new Vector3Int(tilePos.X, tilePos.Y, 0));
        TileBase holeTile  = holeTilemap.GetTile(new Vector3Int(tilePos.X, tilePos.Y, 0));
        GridPosition enemyPos = GetGridPosition(EnemyTankTransform.position);
        return SearchTileType(rockTile,holeTile,enemyPos,tilePos);
    }

    public Vector3 GetPlayer1StartPosition()
    {
        return startPlayerMarker1.gameObject.transform.position;
    }

    public Vector3 GetPlayer2StartPosition()
    {
        return startPlayerMarker2.gameObject.transform.position;
    }

    private TileType SearchTileType(TileBase rockTile,TileBase holeTile,GridPosition enemyPos,GridPosition tilePos)
    {
        if (enemyPos.X == tilePos.X && enemyPos.Y == tilePos.Y ) return TileType.tank;

        if (rockTile == null && holeTile == null) return TileType.empty;
        if (rockTile == null)
        {
            if (holeTile.name.Contains("rock")) return TileType.rock;
            if (holeTile.name.Contains("hole")) return TileType.hole;
        }
        else
        {
            if (rockTile.name.Contains("rock")) return TileType.rock;
            if (rockTile.name.Contains("hole")) return TileType.hole;
        }
        return TileType.empty;
    }
}