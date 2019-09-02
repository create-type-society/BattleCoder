using BattleCoder.GameObject.BotApplication.BulletApplication.Bullet;
using DefaultNamespace;
using UnityEngine;

public class RemoteHostBotController : IBotController
{
    readonly BotApplication botApplication;

    public RemoteHostBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
        SoundManager soundManager, GameSignalingClient gameSignalingClient,
        MeleeAttackEntity meleeAttackEntity)
    {
        var botEntity = Object.Instantiate(botEntityPrefab);
        tileMapInfo.EnemyTankTransform = botEntity.transform;
        botEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBot");
        botEntity.transform.position = tileMapInfo.GetPlayer1StartPosition();
        var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        MeleeAttackApplication meleeAttackApplication = new MeleeAttackApplication(meleeAttackEntity, soundManager);
        botApplication = new BotApplication(botEntity, botEntityAnimation, tileMapInfo,
            new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("EnemyBullet")), soundManager,
            meleeAttackApplication);

        gameSignalingClient.ReceivedClientReceiveSignalData += data =>
        {
            if (data.commandApplyTarget == MatchType.Host)
                new BotCommandsTransformerService().FromCommandData(data.commandData, botApplication);
        };
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