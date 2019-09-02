using UnityEngine;

public class SinglePlayGame : IPlayGame
{
    readonly IBotController playerBotController;
    readonly IBotController enemyBotController;

    public SinglePlayGame(PlayGameInitData playGameInitData)
    {
        var playerMeleeAttackEntity = Object.Instantiate(playGameInitData.meleeAttackPrefab);
        playerMeleeAttackEntity.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");

        var enemyMeleeAttackEntity = Object.Instantiate(playGameInitData.meleeAttackPrefab);
        enemyMeleeAttackEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBullet");

        playerBotController = new PlayerBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.cameraFollower,
            playGameInitData.playerHpPresenter,
            playGameInitData.runButtonEvent,
            playGameInitData.scriptText,
            playGameInitData.errorMsg,
            playGameInitData.soundManager,
            playerMeleeAttackEntity,
            playGameInitData.processScrollViewPresenter
        );

        enemyBotController = new CpuBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.soundManager,
            enemyMeleeAttackEntity
        );
    }

    public void Update()
    {
        playerBotController.Update();
        enemyBotController.Update();
        CheckDeath();
    }

    public void Dispose()
    {
        playerBotController.Dispose();
        enemyBotController.Dispose();
    }

    void CheckDeath()
    {
        if (playerBotController.IsDeath())
        {
            SceneChangeManager.ChangeResultScene(false);
        }

        if (enemyBotController.IsDeath())
        {
            SceneChangeManager.ChangeResultScene(true);
        }
    }
}