using BattleCoder.Map;
using UnityEngine;

namespace BattleCoder.Test.Map
{
    public class MapInfoTest : MonoBehaviour
    {
        private TileMapInfo _info;

        private void Start()
        {
            _info = GetComponent<TileMapInfo>();
            GridPosition p1 = _info.GetGridPosition(new Vector3(-208, -160));
            GridPosition p11 = new GridPosition(1, 1);
            Debug.Assert(_info.GetTileType(p1) == TileType.rock);
            Debug.Assert(_info.GetTileType(_info.GetGridPosition(_info.GetPlayer1StartPosition())) == TileType.empty);
            Debug.Assert(_info.GetTileType(_info.GetGridPosition(_info.GetPlayer2StartPosition())) == TileType.empty);
            Debug.Log(_info.GetTileType(new GridPosition(0, 0)));
        }
    }
}