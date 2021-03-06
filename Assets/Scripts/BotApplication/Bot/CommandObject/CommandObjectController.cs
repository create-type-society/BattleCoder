﻿//コマンドオブジェクト群を管理する

using System.Collections.Generic;
using System.Threading.Tasks;
using BattleCoder.Common;
using BattleCoder.Map;

namespace BattleCoder.BotApplication.Bot.CommandObject
{
    public class CommandObjectController
    {
        readonly CommandObjectQueue<bool> boolUnityFuncCommandObjectQueue = new CommandObjectQueue<bool>();
        readonly CommandObjectQueue<Void> moveTypeCommandObjectQueue = new CommandObjectQueue<Void>();
        readonly CommandObjectQueue<Void> moveShotRotationCommandObjectQueue = new CommandObjectQueue<Void>();
        readonly CommandObjectQueue<Void> actionCommandObjectQueue = new CommandObjectQueue<Void>();
        readonly CommandObjectQueue<GridPosition> posGetCommandObjectQueue = new CommandObjectQueue<GridPosition>();
        readonly CommandObjectQueue<float> radGetCommandObjectQueue = new CommandObjectQueue<float>();
        readonly CommandObjectQueue<TileType> tileTypeGetCommandObjectQueue = new CommandObjectQueue<TileType>();
        List<CoroutineCommandObject> coroutineCommandObjects = new List<CoroutineCommandObject>();

        //移動系のコマンドを登録する
        public Task<Void> AddMoveTypeCommandObject(ICommandObject<Void> commandObject)
            => moveTypeCommandObjectQueue.Run(commandObject);

        //射撃角度変更のコマンドを登録する
        public Task<Void> AddMoveShotRotationCommandObject(MoveShotRotationCommandObject commandObject)
            => moveTypeCommandObjectQueue.Run(commandObject);

        //座標取得系のコマンドを登録する
        public Task<GridPosition> AddPosGetCommandObject(ICommandObject<GridPosition> commandObject)
            => posGetCommandObjectQueue.Run(commandObject);

        //角度取得系のコマンドを登録する
        public Task<float> AddRadGetCommandObject(ICommandObject<float> commandObject)
            => radGetCommandObjectQueue.Run(commandObject);

        //タイルタイプ取得系のコマンドを登録する
        public Task<TileType> AddTileTypeGetCommandObject(ICommandObject<TileType> commandObject)
            => tileTypeGetCommandObjectQueue.Run(commandObject);

        //タイルタイプ取得系のコマンドを登録する
        public Task<bool> AddUnityFuncCommandObject(UnityFunctionCommandObject<bool> commandObject)
            => boolUnityFuncCommandObjectQueue.Run(commandObject);

        public Task<bool> AddGetKeyCommandObject(GetKeyCommandObject commandObject)
            => boolUnityFuncCommandObjectQueue.Run(commandObject);

        //コルーチンコマンドを登録する
        public void AddCoroutineCommandObject(CoroutineCommandObject commandObject)
        {
            coroutineCommandObjects.Add(commandObject);
        }

        public Task<Void> AddActionCommandObject(ICommandObject<Void> commandObject)
        {
            return actionCommandObjectQueue.Run(commandObject);
        }


        //持っているコマンドオブジェクトを全部実行する
        public void RunCommandObjects()
        {
            for (int i = 0; i <15000; i++)
            {
                coroutineCommandObjects.ForEach(x => x.Run());
                boolUnityFuncCommandObjectQueue.Update();
                moveShotRotationCommandObjectQueue.Update();
                posGetCommandObjectQueue.Update();
                radGetCommandObjectQueue.Update();
                tileTypeGetCommandObjectQueue.Update();
                actionCommandObjectQueue.Update();
            }
            
            moveTypeCommandObjectQueue.Update();

        }
    }
}