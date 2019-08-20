using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour
{
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TileInfo tileInfo = new TileInfo(tilemap);
        tileInfo.GetTile(0, 0);
    }
}