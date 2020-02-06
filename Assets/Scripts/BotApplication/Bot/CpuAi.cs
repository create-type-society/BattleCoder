using BattleCoder.Common;
using BattleCoder.Map;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleCoder.BotApplication.Bot
{
    public class CpuAi
    {
        public async void Start(IBotCommands botCommands)
        {
            while (true)
            {
                var direction = (Direction) Random.Range(0, 4);
                var addvec = new Vector2[]
                {
                    Vector2.up, Vector2.down, Vector2.left, Vector2.right
                }[(int) direction];
                var myPosition = await botCommands.GetMyPosition();

                var tileType =
                    await botCommands.GetTileType(new GridPosition((int) (myPosition.X + addvec.x),
                        (int) (myPosition.Y + addvec.y)));
                if (tileType != TileType.Hole)
                    await botCommands.Move(direction, 1);


                if (Random.Range(0, 100) == 50)
                {
                    botCommands.Shot();
                    await botCommands.MoveShotRotation(Random.Range(0, 360));
                }
            }
        }
    }
}