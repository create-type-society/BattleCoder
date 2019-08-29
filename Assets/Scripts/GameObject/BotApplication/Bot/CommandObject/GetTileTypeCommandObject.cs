using BattleCoder.Map;

namespace BattleCoder.GameObject.BotApplication.Bot.CommandObject
{
    public class GetTileTypeCommandObject : BaseCommandObject<TileType>
    {
        readonly TileMapInfo tileMapInfo;
        readonly GridPosition gridPosition;

        public GetTileTypeCommandObject(TileMapInfo tileMapInfo, GridPosition gridPosition)
        {
            this.tileMapInfo = tileMapInfo;
            this.gridPosition = gridPosition;
        }

        public override void Run()
        {
            result = tileMapInfo.GetTileType(gridPosition);
            Finished();
        }
    }
}