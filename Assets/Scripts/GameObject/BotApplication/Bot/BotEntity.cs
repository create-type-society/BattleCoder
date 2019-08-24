using BattleCoder.Map;
using UnityEngine;

public class BotEntity : MonoBehaviour
{
    //横に移動する(岩があったら停止してfalse返す)
    public bool MoveX(float x, TileMapInfo tileMapInfo)
    {
        return Move(x, 0, tileMapInfo);
    }

    //縦に移動する(岩があったら停止してfalse返す)
    public bool MoveY(float y, TileMapInfo tileMapInfo)
    {
        return Move(0, y, tileMapInfo);
    }

    bool Move(float x, float y, TileMapInfo tileMapInfo)
    {
        if (RockCheck(x, y, tileMapInfo)) return false;
        transform.position += new Vector3(x, y, 0);
        return true;
    }

    bool RockCheck(float x, float y, TileMapInfo tileMapInfo)
    {
        var gridSizeHalf = Global.GridSize / 2;
        x = x > 0 ? x + gridSizeHalf : x < 0 ? x - gridSizeHalf : x;
        y = y > 0 ? y + gridSizeHalf : y < 0 ? y - gridSizeHalf : y;
        var tileType = tileMapInfo.GetTileType(transform.position + new Vector3(x, y, 0));
        return tileType == TileType.rock;
    }
}