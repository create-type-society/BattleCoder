//ボットを外部から簡単に制御できるようにするクラス

using System;

public class BotApplication : IBotCommands
{
    readonly BotEntity botEntity;
    readonly BotEntityAnimation botEntityAnimation;
    readonly CommandObjectController commandObjectController = new CommandObjectController();

    public BotApplication(BotEntity botEntity, BotEntityAnimation botEntityAnimation)
    {
        this.botEntity = botEntity;
        this.botEntityAnimation = botEntityAnimation;
    }

    //移動コマンドの発行
    public void Move(Direction direction, float speed, uint gridDistance)
    {
        var moveCommandObject = new MoveCommandObject(botEntity, botEntityAnimation, direction, speed, gridDistance);
        commandObjectController.AddMoveTypeCommandObject(moveCommandObject);
    }

    //コルーチンコマンドの発行
    public void Coroutine(uint frameTime, Action action)
    {
        var coroutineCommandObject = new CoroutineCommandObject(frameTime, action);
        commandObjectController.AddCoroutineCommandObject(coroutineCommandObject);
    }

    //色々な更新
    //毎フレーム1回だけ呼んでください
    public void Update()
    {
        commandObjectController.RunCommandObjects();
    }
}