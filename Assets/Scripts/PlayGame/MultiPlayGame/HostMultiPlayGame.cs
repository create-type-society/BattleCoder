using UnityEngine;

public class HostMultiPlayGame : IPlayGame
{
    readonly IBotController playerBotController;
    readonly IBotController enemyBotController;
    readonly GameSignalingHost gameSignalingHost;

    public HostMultiPlayGame(PlayGameInitData playGameInitData, MultiGameInfo multiGameInfo)
    {
        gameSignalingHost =
            new GameSignalingHost(multiGameInfo.myTcpClient, SelectedStageData.GetSelectedStageKind());

        var playerMeleeAttackEntity = Object.Instantiate(playGameInitData.meleeAttackPrefab);
        playerMeleeAttackEntity.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");

        var enemyMeleeAttackEntity = Object.Instantiate(playGameInitData.meleeAttackPrefab);
        enemyMeleeAttackEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBullet");


        playerBotController = new HostBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.cameraFollower,
            playGameInitData.playerHpPresenter,
            playGameInitData.runButtonEvent,
            playGameInitData.scriptText,
            playGameInitData.errorMsg,
            playGameInitData.soundManager,
            gameSignalingHost,
            playerMeleeAttackEntity,
            playGameInitData.processScrollViewPresenter
        );
        enemyBotController = new RemoteClientBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.soundManager,
            gameSignalingHost,
            enemyMeleeAttackEntity
        );
    }

    public void Update()
    {
        playerBotController.Update();
        enemyBotController.Update();
        CheckDeath();
        gameSignalingHost.Update();
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
            gameSignalingHost.SendBattleResult(BattleResult.YouWin);
            SceneChangeManager.ChangeResultScene(false);
        }

        if (enemyBotController.IsDeath())
        {
            gameSignalingHost.SendBattleResult(BattleResult.YouLose);
            SceneChangeManager.ChangeResultScene(true);
        }

        gameSignalingHost.Dispose();
    }
}