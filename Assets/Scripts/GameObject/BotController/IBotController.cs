using System;

public interface IBotController : IDisposable
{
    void Update();

    bool IsDeath();
}