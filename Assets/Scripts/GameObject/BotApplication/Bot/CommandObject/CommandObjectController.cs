//コマンドオブジェクト群を管理する

public class CommandObjectController
{
    ICommandObject moveTypeCommandObject;

    //移動系のコマンドを登録する
    public void AddMoveTypeCommandObject(ICommandObject commandObject)
    {
        moveTypeCommandObject = commandObject;
    }

    //持っているコマンドオブジェクトを全部実行する
    public void RunCommandObjects()
    {
        if (moveTypeCommandObject != null)
        {
            moveTypeCommandObject.Run();
            if (moveTypeCommandObject.IsFinished)
                moveTypeCommandObject = null;
        }
    }
}