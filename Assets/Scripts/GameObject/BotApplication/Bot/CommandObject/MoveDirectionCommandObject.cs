// 向きの移動

public class MoveDirectionCommandObject : ICommandObject
{
    readonly BotEntity botEntity;
    private readonly BotEntityAnimation botEntityAnimation;
    readonly Direction direction;

    public bool IsFinished { get; private set; }

    public MoveDirectionCommandObject(BotEntity botEntity, BotEntityAnimation botEntityAnimation, Direction direction)
    {
        this.botEntity = botEntity;
        this.botEntityAnimation = botEntityAnimation;
        this.direction = direction;
    }

    public void Run()
    {
        botEntityAnimation.MoveAnimation(direction, false);
        IsFinished = true;
    }
}