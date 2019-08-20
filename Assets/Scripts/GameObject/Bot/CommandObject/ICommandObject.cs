//ボットが実行するコマンドのオブジェクトを表す

public interface ICommandObject
{
    //コマンドが終了しているか
    bool IsFinished { get; }

    //コマンド実行
    void Run();
}