using System.Collections.Concurrent;
using System.Threading.Tasks;

public class CommandObjectQueue<T>
{
    readonly ConcurrentQueue<ICommandObject<T>> commandObjectQueue = new ConcurrentQueue<ICommandObject<T>>();

    ICommandObject<T> runningCommandObject;

    //コマンドを登録する
    public Task<T> Run(ICommandObject<T> commandObject)
    {
        commandObjectQueue.Enqueue(commandObject);
        return Task.Run(commandObject.WaitFinished);
    }

    //コマンドオブジェクトを実行する
    public void Update()
    {
        while (true)
        {
            if (runningCommandObject != null)
                runningCommandObject.Run();
            else if (runningCommandObject == null)
                if (commandObjectQueue.TryDequeue(out runningCommandObject))
                    runningCommandObject.Run();
            if (runningCommandObject == null || runningCommandObject.IsFinished == false)
                return;
            runningCommandObject = null;
        }
    }
}