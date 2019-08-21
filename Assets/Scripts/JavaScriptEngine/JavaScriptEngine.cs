//javaScriptのインタプリタエンジン

using System;
using Jint;
using Jint.Runtime.Interop;

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
    }

    public void ExecuteJS(string script)
    {
        engine.Execute(script);
    }
}