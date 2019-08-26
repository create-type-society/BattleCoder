//移動をするコマンドオブジェクト

using System;

public class MoveCommandObject : ICommandObject
{
    readonly BotEntity botEntity;
    private readonly BotEntityAnimation botEntityAnimation;
    int moveCount;
    int dirChenge;
    bool useCallback;
    readonly float speed = 1;
    readonly Direction direction;
    readonly Action directionChangeCallback;
    readonly TileMapInfo tileMapInfo;
    readonly Action movingCallback;

    public bool IsFinished { get; private set; } = false;

    public MoveCommandObject(BotEntity botEntity, BotEntityAnimation botEntityAnimation, Direction direction,
        Action directionChangeCallback, uint gridDistance, TileMapInfo tileMapInfo,
        Action movingCallback)
    {
        this.botEntity = botEntity;
        this.botEntityAnimation = botEntityAnimation;
        this.direction = direction;
        this.directionChangeCallback = directionChangeCallback;
        this.tileMapInfo = tileMapInfo;
        this.movingCallback = movingCallback;

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
                botEntity.MoveY(speed, tileMapInfo);
                break;
            case Direction.Down:
                botEntity.MoveY(-speed, tileMapInfo);
                break;
            case Direction.Left:
                botEntity.MoveX(-speed, tileMapInfo);
                break;
            case Direction.Right:
                botEntity.MoveX(speed, tileMapInfo);
                break;
        }

        movingCallback();
    }
}