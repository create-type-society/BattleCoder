using BattleCoder.Map;
using UnityEngine;

namespace BattleCoder.GameObject.BotApplication.Bot.CommandObject
{
    public class GetPositionRadianCommandObject : BaseCommandObject<float>
    {
        readonly TileMapInfo tileMapInfo;
        readonly BotEntity botEntity;
        readonly GridPosition targetGridPos;

        public GetPositionRadianCommandObject(TileMapInfo tileMapInfo, BotEntity botEntity, GridPosition targetGridPos)
        {
            this.tileMapInfo = tileMapInfo;
            this.botEntity = botEntity;
            this.targetGridPos = targetGridPos;
        }


        public override void Run()
        {
            var pos = tileMapInfo.GetGridPosition(botEntity.transform.position);
            var x = targetGridPos.X - pos.X;
            var y = targetGridPos.Y - pos.Y;

            result = -Mathf.Atan2(x, y) * 180f / Mathf.PI;
            Finished();
        }
    }
}