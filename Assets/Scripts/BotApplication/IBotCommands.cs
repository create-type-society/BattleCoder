using System;
using System.Threading.Tasks;
using BattleCoder.Common;
using BattleCoder.Map;
using Void = BattleCoder.Common.Void;

namespace BattleCoder.BotApplication
{
    public interface IBotCommands
    {
        //移動コマンド
        Task<Void> Move(Direction direction, uint gridDistance);

        //コルーチン生成実行コマンド
        void Coroutine(uint frameTime, Action action);

        Task<Void> MoveDirection(Direction direction);

        Task<Void> MoveShotRotation(float rotation);

        Task<GridPosition> GetMyPosition();

        Task<float> GetPositionRadian(GridPosition position);

        Task<TileType> GetTileType(GridPosition position);

        Task<bool> Shot();

        Task<Void> MeleeAttack();

        Task<bool> BoolUnityFunc(Func<bool> f);
    }
}