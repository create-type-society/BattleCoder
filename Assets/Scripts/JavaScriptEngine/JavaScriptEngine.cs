//javaScriptのインタプリタエンジン

using System;
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
        engine = new Engine();
        engine.SetValue("Direction", TypeReference.CreateTypeReference(engine, typeof(Direction)));
        engine.SetValue("Move", new Action<Direction, float, uint>(botCommands.Move));
        engine.SetValue("Coroutine",
            new Action<uint, JsValue>((frameTime, jsfunc) =>
            {
                botCommands.Coroutine(frameTime, () => jsfunc.Invoke());
            }));
    }

    public void ExecuteJS(string script)
    {
        engine.Execute(script);
    }
}