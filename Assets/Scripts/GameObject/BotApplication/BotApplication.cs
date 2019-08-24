//ボットを外部から簡単に制御できるようにするクラス

using System;
using System.Collections.Generic;
using BattleCoder.Map;
using UnityEngine;
using Object = UnityEngine.Object;

public class BotApplication : IBotCommands
{
    readonly BotEntity botEntity;
    readonly BotEntityAnimation botEntityAnimation;
    readonly CommandObjectController commandObjectController = new CommandObjectController();
    readonly TileMapInfo tileMapInfo;
    readonly List<BulletApplication> bulletApplicationList = new List<BulletApplication>();
    readonly BulletEntity bulletPrefab;
    public BotHp Hp { get; private set; }

    private Direction direction;

    public BotApplication(BotEntity botEntity, BotEntityAnimation botEntityAnimation, TileMapInfo tileMapInfo,
        BulletEntity bulletPrefab)
    {
        this.botEntity = botEntity;
        this.botEntityAnimation = botEntityAnimation;
        this.tileMapInfo = tileMapInfo;
        this.bulletPrefab = bulletPrefab;
        Hp = new BotHp(10);

        botEntity.transform.position = tileMapInfo.GetPlayer1StartPosition();
    }

    //移動コマンドの発行
    public void Move(Direction direction, float speed, uint gridDistance)
    {
        Action callback = () => { this.direction = direction; };
        var moveCommandObject =
            new MoveCommandObject(botEntity, botEntityAnimation, direction, callback, speed, gridDistance, tileMapInfo,
                CheckHole);
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

    //射撃する
    public void Shot()
    {
        var bulletEntity = Object.Instantiate(bulletPrefab);
        bulletEntity.transform.position = botEntity.transform.position;
        var bulletApplication = new BulletApplication(bulletEntity, new Vector3(0, 8));
        bulletApplicationList.Add(bulletApplication);
    }

    //色々な更新
    //毎フレーム1回だけ呼んでください
    public void Update()
    {
        commandObjectController.RunCommandObjects();
        bulletApplicationList.ForEach(x => x.Update());

        CheckDeath();
    }

    private void CheckHole()
    {
        var tileType = tileMapInfo.GetTileType(botEntity.transform.position);
        if (tileType == TileType.hole)
        {
            Hp = new BotHp(0);
        }
    }

    private void CheckDeath()
    {
        if (Hp.IsDeath())
        {
            SceneChangeManager.ChangeResultScene(false);
        }
    }
}