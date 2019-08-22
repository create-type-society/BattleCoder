//移動をするコマンドオブジェクト

using System;

public class MoveCommandObject : ICommandObject
{
    readonly BotEntity botEntity;
    private readonly BotEntityAnimation botEntityAnimation;
    int moveCount;
    int dirChenge;
    bool useCallback;
    readonly float speed;
    readonly Direction direction;
    readonly Action directionChangeCallback;

    public bool IsFinished { get; private set; } = false;

    public MoveCommandObject(BotEntity botEntity, BotEntityAnimation botEntityAnimation, Direction direction,
        Action directionChangeCallback, float speed, uint gridDistance)
    {
        this.botEntity = botEntity;
        this.botEntityAnimation = botEntityAnimation;
        this.direction = direction;
        this.directionChangeCallback = directionChangeCallback;
        this.speed = speed;

        moveCount = (int) (gridDistance * Global.GridSize / speed);
    }

    public void Run()
    {
        if (dirChenge <= 5)
        {
            botEntityAnimation.MoveAnimation(direction, false);
            dirChenge++;
            return;
        }

        moveCount--;
        if (moveCount < 0)
        {
            botEntityAnimation.ResetAnimation();
            IsFinished = true;
            return;
        }

        if (useCallback)
        {
            useCallback = true;
            directionChangeCallback();
        }

        botEntityAnimation.MoveAnimation(direction, true);
        switch (direction)
        {
            case Direction.Up:
                botEntity.MoveY(speed);
                break;
            case Direction.Down:
                botEntity.MoveY(-speed);
                break;
            case Direction.Left:
                botEntity.MoveX(-speed);
                break;
            case Direction.Right:
                botEntity.MoveX(speed);
                break;
        }
    }
}