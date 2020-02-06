//ボットを外部から簡単に制御できるようにするクラス

using System;
using System.Threading.Tasks;
using BattleCoder.BotApplication.Bot;
using BattleCoder.BotApplication.Bot.CommandObject;
using BattleCoder.Common;
using BattleCoder.Map;
using UnityEngine;
using Void = BattleCoder.Common.Void;

namespace BattleCoder.BotApplication
{
    public class BotApplication : IBotCommands
    {
        [SerializeField] MeleeAttackApplication.MeleeAttackApplication meleeAttackApplication;
        readonly BotEntity botEntity;
        readonly BotEntityAnimation botEntityAnimation;
        readonly CommandObjectController commandObjectController = new CommandObjectController();
        readonly TileMapInfo tileMapInfo;
        readonly Gun gun;
        readonly bool noPosFix;
        Vector3? newPosition = null;

        public BotHp Hp { get; private set; }

        private Direction direction = Direction.Down;
        private float shotRotation = 180;

        public BotApplication(BotEntity botEntity, BotEntityAnimation botEntityAnimation, TileMapInfo tileMapInfo,
            Gun gun,
            MeleeAttackApplication.MeleeAttackApplication meleeAttackApplication, bool noPosFix = false)
        {
            this.botEntity = botEntity;
            botEntity.HitBulletEvent += (sender, e) => Hp = Hp.DamageHp(1);
            this.botEntityAnimation = botEntityAnimation;
            this.tileMapInfo = tileMapInfo;
            Hp = new BotHp(3);
            this.meleeAttackApplication = meleeAttackApplication;
            this.noPosFix = noPosFix;
            this.gun = gun;
        }

        public void SetPos(Vector2 pos)
        {
            newPosition = new Vector3(pos.x, pos.y, botEntity.transform.position.z);
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
                    CheckHole, noPosFix);
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
        public bool Shot()
        {
            return gun.Shot(botEntity.transform.position, shotRotation);
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
            gun.Update();
            UpdateNewPosition();
        }

        private void CheckHole()
        {
            var tileType = tileMapInfo.GetTileType(botEntity.transform.position);
            if (tileType == TileType.Hole)
            {
                Hp = new BotHp(0);
            }
        }

        void UpdateNewPosition()
        {
            if (newPosition == null) return;
            var oldPostiion = botEntity.transform.position;
            if (Vector3.Distance(newPosition.Value, oldPostiion) < 1)
            {
                botEntity.transform.position = newPosition.Value;
                newPosition = null;
            }
            else botEntity.transform.position += (newPosition.Value - oldPostiion) / 15.0f;
        }
    }
}