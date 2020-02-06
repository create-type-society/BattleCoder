using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using BattleCoder.Map;

public class ClientBotCommandsHook : IBotCommands
{
    readonly IBotCommands botCommands;
    readonly GameSignalingClient gameSignalingClient;
    int currentId = 0;
    ConcurrentDictionary<int, SemaphoreSlim> semaphores = new ConcurrentDictionary<int, SemaphoreSlim>();

    public ClientBotCommandsHook(IBotCommands botCommands, GameSignalingClient gameSignalingClient)
    {
        this.botCommands = botCommands;
        this.gameSignalingClient = gameSignalingClient;
        gameSignalingClient.ReceivedClientReceiveSignalData += data =>
        {
            if (data.commandApplyTarget == MatchType.Host) return;
            if (semaphores.TryRemove(data.commandData.id, out var semaphore))
                semaphore.Release();
        };
    }

    public async Task<Void> Move(Direction direction, uint gridDistance)
    {
        SendCommandData(CommandKind.Move, new object[] {direction, gridDistance});
        return await botCommands.Move(direction, gridDistance);
    }

    public void Coroutine(uint frameTime, Action action)
    {
        botCommands.Coroutine(frameTime, action);
    }

    public async Task<Void> MoveDirection(Direction direction)
    {
        SendCommandData(CommandKind.MoveDirection, new object[] {direction});
        return await botCommands.MoveDirection(direction);
    }

    public async Task<Void> MoveShotRotation(float rotation)
    {
        SendCommandData(CommandKind.MoveShotRotation, new object[] {rotation});
        return await botCommands.MoveShotRotation(rotation);
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

    public async void Shot()
    {
        await SendCommandData(CommandKind.Shot, new object[] { });
        botCommands.Shot();
    }

    public async void MeleeAttack()
    {
        await SendCommandData(CommandKind.MeleeAttack, new object[] { });
        botCommands.MeleeAttack();
    }

    public Task<bool> BoolUnityFunc(Func<bool> f)
    {
        return botCommands.BoolUnityFunc(f);
    }

    Task SendCommandData(CommandKind commandKind, object[] parameters)
    {
        var id = Interlocked.Increment(ref currentId);
        var semaphore = new SemaphoreSlim(0, 1);
        semaphores.TryAdd(id, semaphore);
        gameSignalingClient.SendData(new HostReceiveSignalData(
            new CommandData(id, commandKind, 0, parameters)
        ));
        return semaphore.WaitAsync();
    }
}