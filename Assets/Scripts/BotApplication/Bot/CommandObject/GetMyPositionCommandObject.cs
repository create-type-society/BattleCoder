using BattleCoder.Map;

namespace BattleCoder.BotApplication.Bot.CommandObject
{
    public class GetMyPositionCommandObject : BaseCommandObject<GridPosition>
    {
        readonly TileMapInfo tileMapInfo;
        readonly BotEntity botEntity;

        public GetMyPositionCommandObject(TileMapInfo tileMapInfo, BotEntity botEntity)
        {
            this.tileMapInfo = tileMapInfo;
            this.botEntity = botEntity;
        }

        public override void Run()
        {
            result = tileMapInfo.GetGridPosition(botEntity.transform.position);
            Finished();
        }
    }
}