//コマンドオブジェクト群を管理する

using System.Collections.Generic;

public class CommandObjectController
{
    ICommandObject moveTypeCommandObject;
    ICommandObject moveDirectionCommandObject;
    List<CoroutineCommandObject> coroutineCommandObjects = new List<CoroutineCommandObject>();

    //移動系のコマンドを登録する
    public void AddMoveTypeCommandObject(ICommandObject commandObject)
    {
        moveTypeCommandObject = commandObject;
    }

    //コルーチンコマンドを登録する
    public void AddCoroutineCommandObject(CoroutineCommandObject commandObject)
    {
        coroutineCommandObjects.Add(commandObject);
    }

    public void AddMoveDirectionCommandObject(ICommandObject commandObject)
    {
        moveDirectionCommandObject = commandObject;
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

        if (moveDirectionCommandObject != null)
        {
            moveDirectionCommandObject.Run();
            if (moveDirectionCommandObject.IsFinished)
                moveDirectionCommandObject = null;
        }
    }
}