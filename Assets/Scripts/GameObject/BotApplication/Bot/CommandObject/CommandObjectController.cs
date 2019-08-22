//コマンドオブジェクト群を管理する

using System.Collections.Generic;

public class CommandObjectController
{
    ICommandObject moveTypeCommandObject;
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
    }
}