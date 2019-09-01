using System;

public class UnityFunctionCommandObject<T> : BaseCommandObject<T>
{
    readonly Func<T> f;

    public UnityFunctionCommandObject(Func<T> f)
    {
        this.f = f;
    }

    public override void Run()
    {
        result = this.f();
        Finished();
    }
}