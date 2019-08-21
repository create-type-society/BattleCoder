﻿using System.Collections;
using System.Collections.Generic;
using BattleCoder.Map;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private GridPosition pos = new GridPosition(0,0);

    private TileMapInfo tileInfo;
    // Start is called before the first frame update
    void Start()
    {
        
        tileInfo = gameObject.AddComponent<TileMapInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tileInfo.GetTileType(pos,tilemap));
    }
}