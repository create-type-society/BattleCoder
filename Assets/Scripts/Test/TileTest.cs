using System.Collections;
using System.Collections.Generic;
using BattleCoder.Map;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Tilemaps;

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