using BattleCoder.Map;
using UnityEngine;

namespace BattleCoder.GameObject.BotApplication.Bot.CommandObject
{
    public class GetPositionRadianCommandObject : BaseCommandObject<float>
    {
        readonly TileMapInfo tileMapInfo;
        readonly Vector2 selfPos;
        readonly GridPosition targetGridPos;

        public GetPositionRadianCommandObject(TileMapInfo tileMapInfo, Vector2 selfPos, GridPosition targetGridPos)
        {
            this.tileMapInfo = tileMapInfo;
            this.selfPos = selfPos;
            this.targetGridPos = targetGridPos;
        }


        public override void Run()
        {
            var pos = tileMapInfo.GetGridPosition(selfPos);
            var x = targetGridPos.X - pos.X;
            var y = targetGridPos.Y - pos.Y;

            result = -Mathf.Atan2(x, y) * 180f / Mathf.PI;
            Finished();
        }
    }
}