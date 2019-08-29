using System;


//毎フレーム任意の関数を呼び出すコマンドオブジェクト
public class CoroutineCommandObject : BaseCommandObject<Void>
{
    readonly Action action;
    readonly uint frameTime;
    uint count = 0;

    public CoroutineCommandObject(uint frameTime, Action action)
    {
        this.action = action;
        this.frameTime = frameTime;
    }

    public override void Run()
    {
        count++;
        if (frameTime == count)
        {
            action();
            count = 0;
        }
    }
}