//javaScriptのインタプリタエンジン

using System;
using System.Threading;
using System.Threading.Tasks;
using BattleCoder.Map;
using Jint;
using Jint.Native;
using Jint.Runtime.Debugger;
using Jint.Runtime.Interop;
using UnityEngine;

public class JavaScriptEngine
{
    readonly IBotCommands botCommands;
    string errorText = "";

    public JavaScriptEngine(IBotCommands botCommands)
    {
    }

    public string GetErrorText()
    {
        var temp = errorText;
        errorText = "";
        return temp;
    }

    public Task ExecuteJS(string script, CancellationToken cancellationToken)
    {
        var engine = CreateEngine(cancellationToken);
        return Task.Run(() => engine.Execute(script), cancellationToken)
            .ContinueWith(t =>
            {
                if (t.Exception != null)
                    if (t.Exception.InnerException.GetType() != typeof(OperationCanceledException))
                        errorText = t.Exception.InnerException.Message;
            });
    }

    private Engine CreateEngine(CancellationToken cancellationToken)
    {
        var engine = new Engine(options => { options.DebugMode(); });

        engine.Step += (s, e) =>
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException();
            return StepMode.Into;
        };
        engine.SetValue("KeyCode", TypeReference.CreateTypeReference(engine, typeof(KeyCode)));
        engine.SetValue("GetKey", new Func<KeyCode, bool>(code =>
        {
            var task = botCommands.BoolUnityFunc(() => Input.GetKey(code));
            task.Wait();
            return task.Result;
        }));
        engine.SetValue("GetKeyDown", new Func<KeyCode, bool>(code =>
        {
            var task = botCommands.BoolUnityFunc(() => Input.GetKeyDown(code));
            task.Wait();
            return task.Result;
        }));
        engine.SetValue("GetKeyUp", new Func<KeyCode, bool>(code =>
        {
            var task = botCommands.BoolUnityFunc(() => Input.GetKeyUp(code));
            task.Wait();
            return task.Result;
        }));
        engine.SetValue("Dir", TypeReference.CreateTypeReference(engine, typeof(Direction)));
        engine.SetValue("Pos", TypeReference.CreateTypeReference(engine, typeof(GridPosition)));
        engine.SetValue("TileType", TypeReference.CreateTypeReference(engine, typeof(TileType)));
        engine.SetValue("Coroutine",
            new Action<uint, JsValue>((frameTime, jsfunc) =>
            {
                botCommands.Coroutine(frameTime, () => jsfunc.Invoke());
            }));
        engine.SetValue("Move", new Action<Direction, uint>((dir, mass) =>
            botCommands.Move(dir, mass).Wait()
        ));
        engine.SetValue("MoveDir", new Action<Direction>(dir =>
            botCommands.MoveDirection(dir).Wait()
        ));
        engine.SetValue("ShotDir", new Action<float>(dir =>
            botCommands.MoveShotRotation(dir).Wait()
        ));
        engine.SetValue("GetMyPos", new Func<GridPosition>(() =>
        {
            var task = botCommands.GetMyPosition();
            task.Wait();
            return task.Result;
        }));
        engine.SetValue("GetPosRad", new Func<GridPosition, float>(pos =>
        {
            var task = botCommands.GetPositionRadian(pos);
            task.Wait();
            return task.Result;
        }));
        engine.SetValue("GetTileType", new Func<GridPosition, TileType>(pos =>
        {
            var task = botCommands.GetTileType(pos);
            task.Wait();
            return task.Result;
        }));
        engine.SetValue("Print", new Action<object>(obj =>
            ConsoleLogger.Log(DateTime.Now, 0, obj)
        ));
        engine.SetValue("Wait", new Action<int>((milliSeconds) => Task.Delay(milliSeconds).Wait()));
        return engine;
    }
}