﻿//コマンドオブジェクト群を管理する

using System.Collections.Generic;

public class CommandObjectController
{
    ICommandObject moveTypeCommandObject;
    MoveShotRotationCommandObject _moveShotRotationCommandObject;
    List<CoroutineCommandObject> coroutineCommandObjects = new List<CoroutineCommandObject>();

    //移動系のコマンドを登録する
    public void AddMoveTypeCommandObject(ICommandObject commandObject)
    {
        if (moveTypeCommandObject == null)
            moveTypeCommandObject = commandObject;
    }

    //コルーチンコマンドを登録する
    public void AddCoroutineCommandObject(CoroutineCommandObject commandObject)
    {
        coroutineCommandObjects.Add(commandObject);
    }

    public void AddMoveShotRotationCommandObject(MoveShotRotationCommandObject commandObject)
    {
        if (_moveShotRotationCommandObject == null)
            _moveShotRotationCommandObject = commandObject;
    }

    //持っているコマンドオブジェクトを全部実行する
    public void RunCommandObjects()
    {
        coroutineCommandObjects.ForEach(x => x.Run());

        if (moveTypeCommandObject != null)
        {
            moveTypeCommandObject.Run();
            if (moveTypeCommandObject.IsFinished)
                moveTypeCommandObject = null;
        }

        if (_moveShotRotationCommandObject != null)
        {
            _moveShotRotationCommandObject.Run();
            if (_moveShotRotationCommandObject.IsFinished)
                _moveShotRotationCommandObject = null;
        }
    }
}