//ボットが実行するコマンドのオブジェクトを表す

using System.Threading;

public interface ICommandObject<T>
{
    //コマンドが終了しているか
    bool IsFinished { get; }

    //コマンド実行
    void Run();

    //コマンドの実行が終了するまでスレッドを待機する
    T WaitFinished();
}