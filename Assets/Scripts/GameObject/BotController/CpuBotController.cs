using BattleCoder.GameObject.BotApplication.BulletApplication.Bullet;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class CpuBotController
{
    readonly BotApplication botApplication;
    readonly CpuAi cpuAi = new CpuAi();

    public CpuBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
        SoundManager soundManager)
    {
        var botEntity = Object.Instantiate(botEntityPrefab);
        tileMapInfo.EnemyTankTransform = botEntity.transform;
        botEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBot");
        botEntity.transform.position = tileMapInfo.GetPlayer2StartPosition();
        var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        botApplication = new BotApplication(botEntity, botEntityAnimation, tileMapInfo,
            new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("EnemyBullet")), soundManager);
    }

    public void Update()
    {
        botApplication.Update();
        cpuAi.Update(botApplication);
    }

    public bool ISDeath()
    {
        return botApplication.Hp.IsDeath();
    }
}