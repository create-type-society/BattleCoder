using System;
using System.Threading.Tasks;
using BattleCoder.Map;

public class HostBotCommandsHook : IBotCommands
{
    readonly IBotCommands botCommands;
    readonly GameSignalingHost gameSignalingHost;

    public HostBotCommandsHook(IBotCommands botCommands, GameSignalingHost gameSignalingHost)
    {
        this.botCommands = botCommands;
        this.gameSignalingHost = gameSignalingHost;
    }

    public Task<Void> Move(Direction direction, uint gridDistance)
    {
        SendCommandData(
            new CommandData(0, CommandKind.Move, 0, new object[] {direction, gridDistance})
        );
        return botCommands.Move(direction, gridDistance);
    }

    public void Coroutine(uint frameTime, Action action)
    {
        botCommands.Coroutine(frameTime, action);
    }

    public Task<Void> MoveDirection(Direction direction)
    {
        SendCommandData(
            new CommandData(0, CommandKind.MoveDirection, 0, new object[] {direction})
        );
        return botCommands.MoveDirection(direction);
    }

    public Task<Void> MoveShotRotation(float rotation)
    {
        SendCommandData(
            new CommandData(0, CommandKind.MoveShotRotation, 0, new object[] {rotation})
        );
        return botCommands.MoveShotRotation(rotation);
    }

    public Task<GridPosition> GetMyPosition()
    {
        return botCommands.GetMyPosition();
    }

    public Task<float> GetPositionRadian(GridPosition position)
    {
        return botCommands.GetPositionRadian(position);
    }

    public Task<TileType> GetTileType(GridPosition position)
    {
        return botCommands.GetTileType(position);
    }

    public void Shot()
    {
        SendCommandData(new CommandData(0, CommandKind.Shot, 0, new object[] { }));
        botCommands.Shot();
    }

    public void MeleeAttack()
    {
        SendCommandData(new CommandData(0, CommandKind.MeleeAttack, 0, new object[] { }));
        botCommands.MeleeAttack();
    }

    void SendCommandData(CommandData commandData)
    {
        gameSignalingHost.SendData(new ClientReceiveSignalData(
            commandData,
            MatchType.Host
        ));
    }
}