using BattleCoder.Map;
using UnityEngine;

namespace BattleCoder.GameObject.BotApplication.Bot.CommandObject
{
    public class GetMyPositionCommandObject : BaseCommandObject<GridPosition>
    {
        readonly TileMapInfo tileMapInfo;
        readonly Vector2 position;

        public GetMyPositionCommandObject(TileMapInfo tileMapInfo, Vector2 position)
        {
            this.tileMapInfo = tileMapInfo;
            this.position = position;
        }

        public override void Run()
        {
            result = tileMapInfo.GetGridPosition(position);
            Finished();
        }
    }
}