//コマンドオブジェクト群を管理する

using System.Collections.Generic;
using System.Threading.Tasks;
using BattleCoder.Map;

public class CommandObjectController
{
    readonly CommandObjectQueue<Void> moveTypeCommandObjectQueue = new CommandObjectQueue<Void>();
    readonly CommandObjectQueue<Void> moveShotRotationCommandObjectQueue = new CommandObjectQueue<Void>();
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

    //コルーチンコマンドを登録する
    public void AddCoroutineCommandObject(CoroutineCommandObject commandObject)
    {
        coroutineCommandObjects.Add(commandObject);
    }


    //持っているコマンドオブジェクトを全部実行する
    public void RunCommandObjects()
    {
        coroutineCommandObjects.ForEach(x => x.Run());
        moveTypeCommandObjectQueue.Update();
        moveShotRotationCommandObjectQueue.Update();
        posGetCommandObjectQueue.Update();
        radGetCommandObjectQueue.Update();
        tileTypeGetCommandObjectQueue.Update();
    }
}