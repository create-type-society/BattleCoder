using UnityEngine;

public class ClientMultiPlayGame : IPlayGame
{
    readonly IBotController playerBotController;
    readonly IBotController enemyBotController;
    readonly GameSignalingClient gameSignalingClient;

    public ClientMultiPlayGame(PlayGameInitData playGameInitData, MultiGameInfo multiGameInfo)
    {
        gameSignalingClient = new GameSignalingClient(multiGameInfo.myTcpClient);
        gameSignalingClient.ReceivedBattleResult += CheckDeath;

        var playerMeleeAttackEntity = Object.Instantiate(playGameInitData.meleeAttackPrefab);
        playerMeleeAttackEntity.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");

        var enemyMeleeAttackEntity = Object.Instantiate(playGameInitData.meleeAttackPrefab);
        enemyMeleeAttackEntity.gameObject.layer = LayerMask.NameToLayer("EnemyBullet");

        playerBotController = new ClientBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.cameraFollower,
            playGameInitData.playerHpPresenter,
            playGameInitData.runButtonEvent,
            playGameInitData.scriptText,
            playGameInitData.errorMsg,
            playGameInitData.soundManager,
            gameSignalingClient,
            playerMeleeAttackEntity,
            playGameInitData.processScrollViewPresenter
        );
        enemyBotController = new RemoteHostBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.soundManager,
            gameSignalingClient,
            enemyMeleeAttackEntity
        );
    }

    public void Update()
    {
        playerBotController.Update();
        enemyBotController.Update();
        gameSignalingClient.Update();
    }

    void CheckDeath(BattleResult battleResult)
    {
        if (battleResult == BattleResult.YouLose)
            SceneChangeManager.ChangeResultScene(false);
        else
            SceneChangeManager.ChangeResultScene(true);
        gameSignalingClient.Dispose();
    }
}