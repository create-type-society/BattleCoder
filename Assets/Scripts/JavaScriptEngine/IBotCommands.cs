using System;
using BattleCoder.Map;

public interface IBotCommands
{
    //移動コマンド
    void Move(Direction direction, float speed, uint gridDistance);

    //コルーチン生成実行コマンド
    void Coroutine(uint frameTime, Action action);

    void MoveDirection(Direction direction);

    void MoveShotRotation(float rotation);

    GridPosition GetMyPosition();

    float GetPositionRadian(GridPosition position);
}