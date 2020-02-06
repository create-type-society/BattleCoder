using System;

namespace BattleCoder.PlayGame
{
    public interface IPlayGame : IDisposable
    {
        void Update();
    }
}