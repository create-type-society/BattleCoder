using System;
using UnityEngine;

public interface IBotController : IDisposable
{
    void Update();

    bool IsDeath();

    Vector2 GetPos();
    void SetPos(Vector2 pos);
}