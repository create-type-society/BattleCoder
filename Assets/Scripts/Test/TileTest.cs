using BattleCoder.Map;
using UnityEngine;

namespace BattleCoder.Test
{
    public class TileTest : MonoBehaviour
    {
        private GridPosition pos = new GridPosition(0,0);

        [SerializeField] private TileMapInfo tileInfo;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(tileInfo.GetTileType(new Vector3(-7,-5,0)));
        }
    }
}