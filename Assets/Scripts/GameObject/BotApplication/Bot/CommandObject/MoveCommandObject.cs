public class MoveCommandObject : ICommandObject
{
    readonly BotEntity botEntity;
    private readonly BotEntityAnimation botEntityAnimation;
    int moveCount;
    private bool dirChenge;
    readonly float speed;
    readonly Direction direction;

    public bool IsFinished { get; private set; } = false;

    public MoveCommandObject(BotEntity botEntity, BotEntityAnimation botEntityAnimation, Direction direction,
        float speed, uint gridDistance)
    {
        this.botEntity = botEntity;
        this.botEntityAnimation = botEntityAnimation;
        this.direction = direction;
        this.speed = speed;

        moveCount = (int) (gridDistance * Global.GridSize / speed);
    }

    public void Run()
    {
        if (!dirChenge)
        {
            botEntityAnimation.MoveAnimation(direction, false);
            dirChenge = true;
            return;
        }

        moveCount--;
        if (moveCount < 0)
        {
            botEntityAnimation.ResetAnimation();
            IsFinished = true;
            return;
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