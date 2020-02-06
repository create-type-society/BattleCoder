using System;
using UnityEngine;

namespace BattleCoder.BotController
{
    public interface IBotController : IDisposable
    {
        void Update();

        bool IsDeath();

        Vector2 GetPos();
        void SetPos(Vector2 pos);
    }
}