//ボットを外部から簡単に制御できるようにするクラス

using System;

public class BotApplication : IBotCommands
{
    readonly BotEntity botEntity;
    readonly BotEntityAnimation botEntityAnimation;
    readonly CommandObjectController commandObjectController = new CommandObjectController();
    readonly TileMapInfo tileMapInfo;

    private Direction direction;

    public BotApplication(BotEntity botEntity, BotEntityAnimation botEntityAnimation, TileMapInfo tileMapInfo)
    {
        this.botEntity = botEntity;
        this.botEntityAnimation = botEntityAnimation;
        this.tileMapInfo = tileMapInfo;

        botEntity.transform.position = tileMapInfo.GetPlayer1StartPosition();
    }

    //移動コマンドの発行
    public void Move(Direction direction, float speed, uint gridDistance)
    {
        Action callback = () => { this.direction = direction; };
        var moveCommandObject =
            new MoveCommandObject(botEntity, botEntityAnimation, direction, callback, speed, gridDistance);
        commandObjectController.AddMoveTypeCommandObject(moveCommandObject);
    }

    //コルーチンコマンドの発行
    public void Coroutine(uint frameTime, Action action)
    {
        var coroutineCommandObject = new CoroutineCommandObject(frameTime, action);
        commandObjectController.AddCoroutineCommandObject(coroutineCommandObject);
    }

    //方向転換コマンドの実装
    public void MoveDirection(Direction direction)
    {
        Action callback = () => { this.direction = direction; };
        var moveDirectionCommandObject =
            new MoveDirectionCommandObject(botEntity, botEntityAnimation, direction, callback);
        commandObjectController.AddMoveTypeCommandObject(moveDirectionCommandObject);
    }

    //色々な更新
    //毎フレーム1回だけ呼んでください
    public void Update()
    {
        commandObjectController.RunCommandObjects();
    }
}