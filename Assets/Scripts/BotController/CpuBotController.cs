using BattleCoder.BotApplication;
using BattleCoder.BotApplication.Bot;
using BattleCoder.BotApplication.BulletApplication.Bullet;
using BattleCoder.BotApplication.MeleeAttackApplication;
using BattleCoder.BotApplication.MeleeAttackApplication.MeleeAttack;
using BattleCoder.GamePlayUi;
using BattleCoder.Map;
using BattleCoder.Sound;
using UnityEngine;

namespace BattleCoder.BotController
{
    public class CpuBotController : IBotController
    {
        readonly BotApplication.BotApplication botApplication;
        readonly CpuAi cpuAi = new CpuAi();

        public CpuBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
            SoundManager soundManager, MeleeAttackEntity meleeAttackEntity, EventSystemWatcher eventSystemWatcher)
        {
            var botEntity = Object.Instantiate(botEntityPrefab);
            tileMapInfo.EnemyTankTransform = botEntity.transform;
            botEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBot");
            botEntity.transform.position = tileMapInfo.GetPlayer2StartPosition();
            var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
            MeleeAttackApplication meleeAttackApplication = new MeleeAttackApplication(meleeAttackEntity, soundManager);
            var gun = new Gun(
                soundManager,
                new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("EnemyBullet")),
                true
            );

            botApplication = new BotApplication.BotApplication(
                botEntity, botEntityAnimation, tileMapInfo, eventSystemWatcher, gun, meleeAttackApplication
            );
            cpuAi.Start(botApplication);
        }

        public void Update()
        {
            botApplication.Update();
        }

        public bool IsDeath()
        {
            return botApplication.Hp.IsDeath();
        }

        public Vector2 GetPos()
        {
            return botApplication.GetPos();
        }

        public void SetPos(Vector2 pos)
        {
            botApplication.SetPos(pos);
        }

        public void Dispose()
        {
        }
    }
}