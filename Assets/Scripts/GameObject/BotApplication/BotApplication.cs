public class BotApplication
{
    readonly BotEntity botEntity;
    readonly CommandObjectController commandObjectController = new CommandObjectController();

    public BotApplication(BotEntity botEntity)
    {
        this.botEntity = botEntity;
    }

    public void Move(Direction direction, uint speed, uint gridDistance)
    {
        var moveCommandObject = new MoveCommandObject(botEntity, direction, speed, gridDistance);
        commandObjectController.AddMoveTypeCommandObject(moveCommandObject);
    }

    public void Update()
    {
        commandObjectController.RunCommandObjects();
    }
}