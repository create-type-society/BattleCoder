using System;
using System.Threading.Tasks;
using BattleCoder.Map;

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

    void Shot();

    void MeleeAttack();
}