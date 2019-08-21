using System;


//毎フレーム任意の関数を呼び出すコマンドオブジェクト
public class CoroutineCommandObject : ICommandObject
{
    readonly Action action;
    readonly uint frameTime;
    uint count = 0;
    public bool IsFinished { get; } = false;

    public CoroutineCommandObject(uint frameTime, Action action)
    {
        this.action = action;
        this.frameTime = frameTime;
    }

    public void Run()
    {
        count++;
        if (frameTime == count)
        {
            action();
            count = 0;
        }
    }
}