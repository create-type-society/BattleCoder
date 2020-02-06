using BattleCoder.BotApplication;
using BattleCoder.BotApplication.Bot;
using BattleCoder.BotApplication.BulletApplication.Bullet;
using BattleCoder.BotApplication.MeleeAttackApplication;
using BattleCoder.BotApplication.MeleeAttackApplication.MeleeAttack;
using BattleCoder.BotController.Multi.BotCommandsTransformer;
using BattleCoder.GameSignaling;
using BattleCoder.Map;
using BattleCoder.Matching;
using BattleCoder.Sound;
using UnityEngine;

namespace BattleCoder.BotController.Multi
{
    public class RemoteClientBotController : IBotController
    {
        readonly BotApplication.BotApplication botApplication;

        public RemoteClientBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
            SoundManager soundManager, GameSignalingHost gameSignalingHost,
            MeleeAttackEntity meleeAttackEntity)
        {
            var botEntity = Object.Instantiate(botEntityPrefab);
            tileMapInfo.EnemyTankTransform = botEntity.transform;
            botEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBot");
            botEntity.transform.position = tileMapInfo.GetPlayer2StartPosition();
            var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
            MeleeAttackApplication meleeAttackApplication = new MeleeAttackApplication(meleeAttackEntity, soundManager);
            var gun = new Gun(soundManager, new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("EnemyBullet")));
            botApplication = new BotApplication.BotApplication(
                botEntity, botEntityAnimation, tileMapInfo, gun,
                meleeAttackApplication
            );

            gameSignalingHost.ReceivedHostReceiveSignalData += data =>
            {
                new BotCommandsTransformerService().FromCommandData(data.clientCommandData, botApplication);
                gameSignalingHost.SendData(new ClientReceiveSignalData(data.clientCommandData, MatchType.Client));
            };
        }

        public void Update()
        {
            botApplication.Update();
        }

        public Vector2 GetPos()
        {
            return botApplication.GetPos();
        }

        public void SetPos(Vector2 pos)
        {
            botApplication.SetPos(pos);
        }


        public bool IsDeath()
        {
            return botApplication.Hp.IsDeath();
        }

        public void Dispose()
        {
        }
    }
}