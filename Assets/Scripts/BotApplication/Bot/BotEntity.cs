using System;
using BattleCoder.Map;
using UnityEngine;

public class BotEntity : MonoBehaviour
{
    // 弾当たった時ののイベント
    public event EventHandler<EventArgs> HitBulletEvent;

    //横に移動する(岩か敵があったら停止してfalse返す)
    public bool MoveX(float x, TileMapInfo tileMapInfo)
    {
        return Move(x, 0, tileMapInfo);
    }

    //縦に移動する(岩か敵があったら停止してfalse返す)
    public bool MoveY(float y, TileMapInfo tileMapInfo)
    {
        return Move(0, y, tileMapInfo);
    }

    bool Move(float x, float y, TileMapInfo tileMapInfo)
    {
        if (RockCheck(x, y, tileMapInfo) || TancCheck(x, y, tileMapInfo)) return false;
        transform.position += new Vector3(x, y, 0);
        return true;
    }

    bool RockCheck(float x, float y, TileMapInfo tileMapInfo)
    {
        var gridSizeHalf = Global.GridSize / 2;
        x = x > 0 ? x + gridSizeHalf : x < 0 ? x - gridSizeHalf : x;
        y = y > 0 ? y + gridSizeHalf : y < 0 ? y - gridSizeHalf : y;
        var tileType = tileMapInfo.GetTileType(transform.position + new Vector3(x, y, 0));
        return tileType == TileType.Rock;
    }

    bool TancCheck(float x, float y, TileMapInfo tileMapInfo)
    {
        var gridSizeHalf = Global.GridSize / 2+1;
        x = x > 0 ? x + gridSizeHalf : x < 0 ? x - gridSizeHalf : x;
        y = y > 0 ? y + gridSizeHalf : y < 0 ? y - gridSizeHalf : y;
        GridPosition movePos = tileMapInfo.GetGridPosition(transform.position + new Vector3(x, y, 0));
        var tileType = tileMapInfo.GetTileType(transform.position + new Vector3(x, y, 0));
        if (tileType == TileType.Tank)
        {
            GridPosition myPos = tileMapInfo.GetGridPosition(transform.position);
            if (!(movePos.X == myPos.X && movePos.Y == myPos.Y))
            {
                PosFix(tileMapInfo);
                return true;
            }

            ;
        }

        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HitBulletEvent.Invoke(this, EventArgs.Empty);
    }

    public void PosFix(TileMapInfo tileMapInfo)
    {
        GridPosition myPos = tileMapInfo.GetGridPosition(transform.position);
        var gridMyPos = tileMapInfo.GetWorldPosition(myPos);
        transform.position = new Vector3(gridMyPos.x, gridMyPos.y, transform.position.z);
    }
}