using BattleCoder.GameObject.BotApplication.BulletApplication.Bullet;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class CpuBotController
{
    readonly BotApplication botApplication;

    public CpuBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
        SoundManager soundManager)
    {
        var botEntity = Object.Instantiate(botEntityPrefab);
        botEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBot");
        botEntity.transform.position = tileMapInfo.GetPlayer2StartPosition();
        var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        botApplication = new BotApplication(botEntity, botEntityAnimation, tileMapInfo,
            new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("EnemyBullet")), soundManager);
    }

    public void Update()
    {
        botApplication.Update();
    }

    public bool ISDeath()
    {
        return botApplication.Hp.IsDeath();
    }
}