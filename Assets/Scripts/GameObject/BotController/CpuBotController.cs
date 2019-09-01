﻿using BattleCoder.GameObject.BotApplication.BulletApplication.Bullet;
using UnityEngine;

public class CpuBotController : IBotController
{
    readonly BotApplication botApplication;
    readonly CpuAi cpuAi = new CpuAi();

    public CpuBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
        SoundManager soundManager, MeleeAttackEntity meleeAttackEntity)
    {
        var botEntity = Object.Instantiate(botEntityPrefab);
        tileMapInfo.EnemyTankTransform = botEntity.transform;
        botEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBot");
        botEntity.transform.position = tileMapInfo.GetPlayer2StartPosition();
        var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        MeleeAttackApplication meleeAttackApplication = new MeleeAttackApplication(meleeAttackEntity);
        botApplication = new BotApplication(botEntity, botEntityAnimation, tileMapInfo,
            new BulletEntityCreator(
                bulletPrefab, LayerMask.NameToLayer("EnemyBullet")),
            soundManager, meleeAttackApplication
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
}