//クライアントが受け取るデータの型

public struct ClientReceiveSignalData
{
    readonly public BattleResult battleResult;
    readonly public CommandData commandData;
    readonly public MatchType commandApplyTarget;

    public ClientReceiveSignalData(BattleResult battleResult, CommandData commandData, MatchType commandApplyTarget)
    {
        this.battleResult = battleResult;
        this.commandData = commandData;
        this.commandApplyTarget = commandApplyTarget;
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
    readonly public int id;
    readonly public CommandKind kind;
    readonly public int frameCount;
    readonly public object[] parameters;

    public CommandData(int id, CommandKind kind, int frameCount, object[] parameters)
    {
        this.id = id;
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