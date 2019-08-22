// 向きの移動

using System;

public class MoveDirectionCommandObject : ICommandObject
{
    readonly BotEntity botEntity;
    readonly BotEntityAnimation botEntityAnimation;
    readonly Direction direction;
    readonly Action directionChangeCallback;

    public bool IsFinished { get; private set; }

    public MoveDirectionCommandObject(BotEntity botEntity, BotEntityAnimation botEntityAnimation, Direction direction,
        Action directionChangeCallback)
    {
        this.botEntity = botEntity;
        this.botEntityAnimation = botEntityAnimation;
        this.direction = direction;
        this.directionChangeCallback = directionChangeCallback;
    }

    public void Run()
    {
        directionChangeCallback();
        botEntityAnimation.MoveAnimation(direction, false);
        IsFinished = true;
    }
}