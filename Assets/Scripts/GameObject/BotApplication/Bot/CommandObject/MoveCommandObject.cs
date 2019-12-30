﻿//移動をするコマンドオブジェクト

using System;

public class MoveCommandObject : BaseCommandObject<Void>
{
    readonly BotEntity botEntity;
    private readonly BotEntityAnimation botEntityAnimation;
    int moveCount;
    bool dirChenged = false;
    bool useCallback = true;
    readonly float speed = 1.5f;
    readonly Direction direction;
    readonly Action directionChangeCallback;
    readonly TileMapInfo tileMapInfo;
    readonly Action movingCallback;

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

    public override void Run()
    {
        if (dirChenged == false)
        {
            botEntityAnimation.MoveAnimation(direction, false);
            dirChenged = true;
        }

        moveCount--;

        if (useCallback)
        {
            useCallback = false;
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
        if (moveCount == 0)
        {
            botEntityAnimation.ResetAnimation();
            botEntity.PosFix(tileMapInfo);
            Finished();
        }
    }
}