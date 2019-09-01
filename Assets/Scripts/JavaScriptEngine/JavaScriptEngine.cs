//javaScriptのインタプリタエンジン

using System;
using System.Threading.Tasks;
using BattleCoder.Map;
using Jint;
using Jint.Native;
using Jint.Runtime.Interop;
using UnityEngine;

public class JavaScriptEngine
{
    readonly IBotCommands botCommands;
    readonly Engine engine;
    string errorText = "";

    public JavaScriptEngine(IBotCommands botCommands)
    {
        this.botCommands = botCommands;
        engine = new Engine();
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
        engine.SetValue("Print", new Action<object>(Debug.Log));
        engine.SetValue("Wait", new Action<int>((milliSeconds) => Task.Delay(milliSeconds).Wait()));
    }

    public string GetErrorText()
    {
        var temp = errorText;
        errorText = "";
        return temp;
    }

    public void ExecuteJS(string script)
    {
        Task.Run(() => engine.Execute(script)).ContinueWith(t =>
        {
            if (t.Exception != null)
                errorText = t.Exception.InnerException.Message;
        });
    }
}