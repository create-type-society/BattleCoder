//ボットを外部から簡単に制御できるようにするクラス

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleCoder.GameObject.BotApplication.Bot.CommandObject;
using BattleCoder.Map;
using UnityEngine;
using Object = UnityEngine.Object;

public class BotApplication : IBotCommands
{
    [SerializeField] MeleeAttackApplication meleeAttackApplication;
    readonly BotEntity botEntity;
    readonly BotEntityAnimation botEntityAnimation;
    readonly CommandObjectController commandObjectController = new CommandObjectController();
    readonly TileMapInfo tileMapInfo;
    readonly IBulletEntityCreator bulletEntityCreator;
    readonly SoundManager soundManager;

    List<BulletApplication> bulletApplicationList = new List<BulletApplication>();

    public BotHp Hp { get; private set; }

    private Direction direction = Direction.Down;
    private float shotRotation = 180;

    public BotApplication(BotEntity botEntity, BotEntityAnimation botEntityAnimation, TileMapInfo tileMapInfo,
        IBulletEntityCreator bulletEntityCreator, SoundManager soundManager,
        MeleeAttackApplication meleeAttackApplication)
    {
        this.soundManager = soundManager;
        this.botEntity = botEntity;
        botEntity.HitBulletEvent += (sender, e) => Hp = Hp.DamageHp(1);
        this.botEntityAnimation = botEntityAnimation;
        this.tileMapInfo = tileMapInfo;
        this.bulletEntityCreator = bulletEntityCreator;
        Hp = new BotHp(3);
        this.meleeAttackApplication = meleeAttackApplication;
    }

    public void SetPos(Vector2 pos)
    {
        botEntity.transform.position = new Vector3(pos.x, pos.y, botEntity.transform.position.z);
    }

    public Vector2 GetPos()
    {
        return botEntity.transform.position;
    }


    //移動コマンドの発行
    public Task<Void> Move(Direction direction, uint gridDistance)
    {
        Action callback = () => { this.direction = direction; };
        var moveCommandObject =
            new MoveCommandObject(botEntity, botEntityAnimation, direction, callback, gridDistance, tileMapInfo,
                CheckHole);
        return commandObjectController.AddMoveTypeCommandObject(moveCommandObject);
    }

    //コルーチンコマンドの発行
    public void Coroutine(uint frameTime, Action action)
    {
        var coroutineCommandObject = new CoroutineCommandObject(frameTime, action);
        commandObjectController.AddCoroutineCommandObject(coroutineCommandObject);
    }

    //方向転換コマンドの実装
    public Task<Void> MoveDirection(Direction direction)
    {
        Action callback = () => { this.direction = direction; };
        var moveDirectionCommandObject =
            new MoveDirectionCommandObject(botEntity, botEntityAnimation, direction, callback);
        return commandObjectController.AddMoveTypeCommandObject(moveDirectionCommandObject);
    }

    public Task<Void> MoveShotRotation(float rotation)
    {
        Action callback = () => { this.shotRotation = rotation; };
        var moveShotRotationCommandObject = new MoveShotRotationCommandObject(callback);
        return commandObjectController.AddMoveShotRotationCommandObject(moveShotRotationCommandObject);
    }

    public Task<GridPosition> GetMyPosition()
    {
        return commandObjectController.AddPosGetCommandObject(
            new GetMyPositionCommandObject(tileMapInfo, botEntity)
        );
    }

    public Task<float> GetPositionRadian(GridPosition position)
    {
        return commandObjectController.AddRadGetCommandObject(
            new GetPositionRadianCommandObject(tileMapInfo, botEntity, position)
        );
    }

    public Task<TileType> GetTileType(GridPosition position)
    {
        return commandObjectController.AddTileTypeGetCommandObject(
            new GetTileTypeCommandObject(tileMapInfo, position)
        );
    }

    //射撃する
    public void Shot()
    {
        soundManager.MakeFiringSound();
        var bulletEntity = bulletEntityCreator.Create();
        bulletEntity.transform.position = botEntity.transform.position;
        bulletEntity.transform.rotation = Quaternion.Euler(0, 0, shotRotation);
        var bulletApplication = new BulletApplication(bulletEntity, CalcRotationVector(bulletEntity, 2f));
        bulletApplicationList.Add(bulletApplication);
    }

    //近接攻撃
    public void MeleeAttack()
    {
        meleeAttackApplication.MeleeAttack(botEntity.transform.position, direction);
    }


    //boolを返す任意の関数をメインスレッドで実行する
    public Task<bool> BoolUnityFunc(Func<bool> f)
    {
        return commandObjectController.AddUnityFuncCommandObject(new UnityFunctionCommandObject<bool>(f));
    }

    //色々な更新
    //毎フレーム1回だけ呼んでください
    public void Update()
    {
        meleeAttackApplication.Update();
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
        if (tileType == TileType.Hole)
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