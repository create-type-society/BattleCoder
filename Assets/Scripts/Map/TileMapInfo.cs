using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace BattleCoder.Map
{
    public class TileMapInfo : MonoBehaviour
    {
        [FormerlySerializedAs("tilemap")] [SerializeField]
        private Tilemap rockTilemap;

        [FormerlySerializedAs("tilemap")] [SerializeField]
        private Tilemap holeTilemap;

        [SerializeField] private GameObject startPlayerMarker1;
        [SerializeField] private GameObject startPlayerMarker2;

        public Transform EnemyTankTransform { private get; set; }
        public Transform PlayerTankTransform { private get; set; }

        public GridPosition GetGridPosition(Vector3 worldPos)
        {
            Vector3Int tilePos = rockTilemap.WorldToCell(worldPos);
            return new GridPosition(tilePos.x, tilePos.y);
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition)
        {
            return rockTilemap.CellToWorld(new Vector3Int(gridPosition.X, gridPosition.Y, 0)) +
                   new Vector3(Global.GridSize / 2f, Global.GridSize / 2f, 0f);
        }


        public TileType GetTileType(Vector3 worldPos)
        {
            GridPosition tilePos = GetGridPosition(worldPos);
            TileBase rockTile = rockTilemap.GetTile(new Vector3Int(tilePos.X, tilePos.Y, 0));
            TileBase holeTile = holeTilemap.GetTile(new Vector3Int(tilePos.X, tilePos.Y, 0));
            GridPosition enemyPos = GetGridPosition(EnemyTankTransform.position);
            GridPosition playerPos = GetGridPosition(PlayerTankTransform.position);
            return SearchTileType(rockTile, holeTile, enemyPos, playerPos, tilePos);
        }

        public TileType GetTileType(GridPosition tilePos)
        {
            TileBase rockTile = rockTilemap.GetTile(new Vector3Int(tilePos.X, tilePos.Y, 0));
            TileBase holeTile = holeTilemap.GetTile(new Vector3Int(tilePos.X, tilePos.Y, 0));
            GridPosition enemyPos = GetGridPosition(EnemyTankTransform.position);
            GridPosition playerPos = GetGridPosition(PlayerTankTransform.position);
            return SearchTileType(rockTile, holeTile, enemyPos, playerPos, tilePos);
        }

        public Vector3 GetPlayer1StartPosition()
        {
            return startPlayerMarker1.gameObject.transform.position;
        }

        public Vector3 GetPlayer2StartPosition()
        {
            return startPlayerMarker2.gameObject.transform.position;
        }

        private TileType SearchTileType(TileBase rockTile, TileBase holeTile, GridPosition enemyPos, GridPosition playerPos,
            GridPosition tilePos)
        {
            if (rockTile != null || holeTile != null)
            {
                if (rockTile == null)
                {
                    if (holeTile.name.Contains("rock")) return TileType.Rock;
                    if (holeTile.name.Contains("hole")) return TileType.Hole;
                }
                else
                {
                    if (rockTile.name.Contains("rock")) return TileType.Rock;
                    if (rockTile.name.Contains("hole")) return TileType.Hole;
                }
            }

            if (enemyPos.X == tilePos.X && enemyPos.Y == tilePos.Y) return TileType.Tank;
            if (playerPos.X == tilePos.X && playerPos.Y == tilePos.Y) return TileType.Tank;

            return TileType.Empty;
        }
    }
}