//javaScriptのインタプリタエンジン

using System;
using BattleCoder.Map;
using Jint;
using Jint.Native;
using Jint.Runtime.Interop;
using UnityEngine;

public class JavaScriptEngine
{
    readonly IBotCommands botCommands;
    readonly Engine engine;

    public JavaScriptEngine(IBotCommands botCommands)
    {
        this.botCommands = botCommands;
        engine = new Engine(options => options.TimeoutInterval(TimeSpan.FromMilliseconds(1000.0)));
        engine.SetValue("Dir", TypeReference.CreateTypeReference(engine, typeof(Direction)));
        engine.SetValue("Pos", TypeReference.CreateTypeReference(engine, typeof(GridPosition)));
        engine.SetValue("Move", new Action<Direction, float, uint>(botCommands.Move));
        engine.SetValue("Coroutine",
            new Action<uint, JsValue>((frameTime, jsfunc) =>
            {
                botCommands.Coroutine(frameTime, () => jsfunc.Invoke());
            }));
        engine.SetValue("MoveDir", new Action<Direction>(botCommands.MoveDirection));
        engine.SetValue("ShotDir", new Action<float>(botCommands.MoveShotRotation));
        engine.SetValue("GetMyPos", new Func<GridPosition>(botCommands.GetMyPosition));
        engine.SetValue("GetPosRad", new Func<GridPosition, float>(botCommands.GetPositionRadian));
        engine.SetValue("Print", new Action<object>(Debug.Log));
    }

    public void ExecuteJS(string script)
    {
        engine.Execute(script);
    }
}