//javaScriptのインタプリタエンジン

using System;
using System.Threading;
using System.Threading.Tasks;
using BattleCoder.BotApplication;
using BattleCoder.Common;
using BattleCoder.Console;
using BattleCoder.Map;
using Jint;
using Jint.Native;
using Jint.Runtime.Debugger;
using Jint.Runtime.Interop;
using UnityEngine;

namespace BattleCoder.JavaScriptEngine
{
    public class JavaScriptEngine
    {
        readonly IBotCommands botCommands;
        string errorText = "";

        public JavaScriptEngine(IBotCommands botCommands)
        {
            this.botCommands = botCommands;
        }

        public string GetErrorText()
        {
            var temp = errorText;
            errorText = "";
            return temp;
        }

        public Task ExecuteJS(string script, CancellationToken cancellationToken, int processId)
        {
            var engine = CreateEngine(cancellationToken, processId);
            return Task.Run(() => engine.Execute(script), cancellationToken)
                .ContinueWith(t =>
                {
                    if (t.Exception != null)
                        if (t.Exception.InnerException.GetType() != typeof(OperationCanceledException))
                            errorText = t.Exception.InnerException.Message;
                });
        }

        private Engine CreateEngine(CancellationToken cancellationToken, int processId)
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
                var task = botCommands.GetKey(code);
                task.Wait();
                return task.Result;
            }));
            engine.SetValue("GetKeyDown", new Func<KeyCode, bool>(code =>
            {
                var task = botCommands.GetKeyDown(code);
                task.Wait();
                return task.Result;
            }));
            engine.SetValue("GetKeyUp", new Func<KeyCode, bool>(code =>
            {
                var task = botCommands.GetKeyUp(code);
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
            engine.SetValue("Shot", new Action(() => botCommands.Shot()));
            engine.SetValue("Attack", new Action(() => botCommands.MeleeAttack()));

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
            engine.SetValue("GetPosAngle", new Func<GridPosition, float>(pos =>
            {
                var task = botCommands.GetPositionAngle(pos);
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
                ConsoleLogger.Log(DateTime.Now, processId, obj)
            ));
            engine.SetValue("Wait", new Action<int>((milliSeconds) => Task.Delay(milliSeconds).Wait()));
            return engine;
        }
    }
}