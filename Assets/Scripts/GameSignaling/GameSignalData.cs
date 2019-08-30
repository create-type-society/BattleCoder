//クライアントが受け取るデータの型

public struct ClientReceiveSignalData
{
    readonly public BattleResult battleResult;
    readonly public CommandData commandData;

    public ClientReceiveSignalData(BattleResult battleResult, CommandData commandData)
    {
        this.battleResult = battleResult;
        this.commandData = commandData;
    }
}

//ホストが受け取るデータの型
public struct HostReceiveSignalData
{
    readonly public CommandData clientCommandData;

    public HostReceiveSignalData(CommandData clientCommandData)
    {
        this.clientCommandData = clientCommandData;
    }
}

//バトル勝敗判定
public enum BattleResult
{
    YouWin,
    YouLose,
    Wait
}

//コマンドデータ
public struct CommandData
{
    readonly public CommandKind kind;
    readonly public int frameCount;
    readonly public object[] parameters;

    public CommandData(CommandKind kind, int frameCount, object[] parameters)
    {
        this.kind = kind;
        this.frameCount = frameCount;
        this.parameters = parameters;
    }
}

//コマンドの種類
public enum CommandKind
{
    Move,
    MoveDirection,
    MoveShotRotation,
    Shot,
}