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
            Object.Instantiate(playGameInitData.meleeAttackPrefab)
        );
        enemyBotController = new RemoteHostBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.soundManager,
            gameSignalingClient,
            Object.Instantiate(playGameInitData.meleeAttackPrefab)
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
    }
}