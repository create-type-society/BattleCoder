﻿using BattleCoder.BotApplication;
using BattleCoder.BotApplication.Bot;
using BattleCoder.BotApplication.BulletApplication.Bullet;
using BattleCoder.BotApplication.MeleeAttackApplication;
using BattleCoder.BotApplication.MeleeAttackApplication.MeleeAttack;
using BattleCoder.BotController.Multi.BotCommandsTransformer;
using BattleCoder.GamePlayUi;
using BattleCoder.GameSignaling;
using BattleCoder.Map;
using BattleCoder.Matching;
using BattleCoder.Sound;
using UnityEngine;

namespace BattleCoder.BotController.Multi
{
    public class RemoteHostBotController : IBotController
    {
        readonly BotApplication.BotApplication botApplication;

        public RemoteHostBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
            SoundManager soundManager, GameSignalingClient gameSignalingClient,
            MeleeAttackEntity meleeAttackEntity, EventSystemWatcher eventSystemWatcher)
        {
            var botEntity = Object.Instantiate(botEntityPrefab);
            tileMapInfo.EnemyTankTransform = botEntity.transform;
            botEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBot");
            botEntity.transform.position = tileMapInfo.GetPlayer1StartPosition();
            var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
            MeleeAttackApplication meleeAttackApplication = new MeleeAttackApplication(meleeAttackEntity, soundManager);
            var gun = new Gun(
                soundManager,
                new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("EnemyBullet")),
                true
            );

            botApplication = new BotApplication.BotApplication(
                botEntity, botEntityAnimation, tileMapInfo, eventSystemWatcher, gun,
                meleeAttackApplication, true
            );

            gameSignalingClient.ReceivedClientReceiveSignalData += data =>
            {
                if (data.commandApplyTarget == MatchType.Host)
                    new BotCommandsTransformerService().FromCommandData(data.commandData, botApplication);
            };
        }

        public Vector2 GetPos()
        {
            return botApplication.GetPos();
        }

        public void SetPos(Vector2 pos)
        {
            botApplication.SetPos(pos);
        }


        public void Update()
        {
            botApplication.Update();
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