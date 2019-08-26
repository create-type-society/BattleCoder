using System;
using BattleCoder.Map;
using UnityEngine;
using Random = UnityEngine.Random;

public class CpuAi
{
    public void Update(IBotCommands botCommands)
    {
        var direction = (Direction) Random.Range(0, 4);
        var addvec = new Vector2[]
        {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        }[(int) direction];
        var myPosition = botCommands.GetMyPosition();

        var tileType =
            botCommands.GetTileType(new GridPosition((int) (myPosition.X + addvec.x), (int) (myPosition.Y + addvec.y)));
        if (tileType != TileType.hole)
            botCommands.Move(direction, 1);


        if (Random.Range(0, 100) == 50)
        {
            botCommands.Shot();
            botCommands.MoveShotRotation(Random.Range(0, 360));
        }
    }
}