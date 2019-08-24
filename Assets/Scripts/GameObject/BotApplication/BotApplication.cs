//ボットを外部から簡単に制御できるようにするクラス

using System;
using System.Collections.Generic;
using System.Linq;
using BattleCoder.Map;
using UnityEngine;
using Object = UnityEngine.Object;

public class BotApplication : IBotCommands
{
    readonly BotEntity botEntity;
    readonly BotEntityAnimation botEntityAnimation;
    readonly CommandObjectController commandObjectController = new CommandObjectController();
    readonly TileMapInfo tileMapInfo;
    readonly BulletEntity bulletPrefab;

    List<BulletApplication> bulletApplicationList = new List<BulletApplication>();

    public BotHp Hp { get; private set; }

    private Direction direction;
    private float shotRotation;

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

    public void MoveShotRotation(float rotation)
    {
        Action callback = () => { this.shotRotation = rotation; };
        var moveShotRotationCommandObject = new MoveShotRotationCommandObject(callback);
        commandObjectController.AddMoveShotRotationCommandObject(moveShotRotationCommandObject);
    }

    public GridPosition GetMyPosition()
    {
        return tileMapInfo.GetGridPosition(botEntity.transform.position);
    }

    //射撃する
    public void Shot()
    {
        var bulletEntity = Object.Instantiate(bulletPrefab);
        bulletEntity.transform.position = botEntity.transform.position;
        bulletEntity.transform.rotation = Quaternion.Euler(0, 0, shotRotation);
        var bulletApplication = new BulletApplication(bulletEntity, CalcRotationVector(bulletEntity, 2f));
        bulletApplicationList.Add(bulletApplication);
    }

    //色々な更新
    //毎フレーム1回だけ呼んでください
    public void Update()
    {
        commandObjectController.RunCommandObjects();
        bulletApplicationList.ForEach(x => x.Update());
        bulletApplicationList = bulletApplicationList.Where(x =>
        {
            if (x.DeleteFlag) x.Delete();
            return x.DeleteFlag == false;
        }).ToList();
    }

    private void CheckHole()
    {
        var tileType = tileMapInfo.GetTileType(botEntity.transform.position);
        if (tileType == TileType.hole)
        {
            Hp = new BotHp(0);
        }
    }

    private Vector3 CalcRotationVector(BulletEntity bulletEntity, float speed)
    {
        var r = bulletEntity.transform.rotation.eulerAngles.z;
        var x = -Mathf.Sin(ToRadians(r));
        var y = Mathf.Cos(ToRadians(r));
        return new Vector3(x, y) * speed;
    }

    private float ToRadians(float angle)
    {
        return Mathf.PI / 180f * angle;
    }
}