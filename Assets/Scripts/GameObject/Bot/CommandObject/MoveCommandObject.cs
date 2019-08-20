public class MoveCommandObject : ICommandObject
{
    readonly BotEntity botEntity;
    int moveCount;
    readonly float speed;
    readonly Direction direction;

    public bool IsFinished { get; private set; } = false;

    public MoveCommandObject(BotEntity botEntity, Direction direction, float speed, uint gridDistance)
    {
        this.botEntity = botEntity;
        this.direction = direction;
        this.speed = speed;

        moveCount = (int) (gridDistance * Global.GridSize / speed);
    }

    public void Run()
    {
        moveCount--;
        if (moveCount < 0)
        {
            IsFinished = true;
            return;
        }

        switch (direction)
        {
            case Direction.Up:
                botEntity.MoveY(-speed);
                break;
            case Direction.Down:
                botEntity.MoveY(speed);
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