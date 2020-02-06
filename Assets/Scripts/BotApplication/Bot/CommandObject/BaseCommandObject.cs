using System.Threading;

namespace BattleCoder.BotApplication.Bot.CommandObject
{
    public abstract class BaseCommandObject<T> : ICommandObject<T>
    {
        SemaphoreSlim finisheSemaphore = new SemaphoreSlim(0, 1);

        protected T result;

        public bool IsFinished { get; private set; }

        public abstract void Run();

        public void Finished()
        {
            IsFinished = true;
            finisheSemaphore.Release();
        }

        public T WaitFinished()
        {
            finisheSemaphore.Wait();
            return result;
        }
    }
}