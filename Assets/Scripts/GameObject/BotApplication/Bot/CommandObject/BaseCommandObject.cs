using System.Threading;

public abstract class BaseCommandObject<T> : ICommandObject<T>
{
    protected T result;

    public bool IsFinished { get; private set; }

    public abstract void Run();

    public void Finished()
    {
        IsFinished = true;
    }

    public T WaitFinished()
    {
        while (!IsFinished) ;
        return result;
    }
}