using System;
using Object = UnityEngine.Object;

public class MultiPlayGame : IPlayGame
{
    readonly IBotController playerBotController;
    readonly IBotController enemyBotController;

    readonly Action GameSignalingUpdater;

    public MultiPlayGame(PlayGameInitData playGameInitData)
    {
        var multiGameInfo = StartGameInfo.GetMultiGameInfo();
        if (multiGameInfo.matchType == MatchType.Host)
        {
            var gameSignalingHost =
                new GameSignalingHost(multiGameInfo.myTcpClient, SelectedStageData.GetSelectedStageKind());

            GameSignalingUpdater = gameSignalingHost.Update;

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
                Object.Instantiate(playGameInitData.meleeAttackPrefab)
            );
            enemyBotController = new CpuBotController(
                playGameInitData.botEntityPrefab,
                playGameInitData.tileMapInfo,
                playGameInitData.bulletPrefab,
                playGameInitData.soundManager,
                Object.Instantiate(playGameInitData.meleeAttackPrefab)
            );
        }
        else
        {
            var gameSignalingClient = new GameSignalingClient(multiGameInfo.myTcpClient);

            GameSignalingUpdater = gameSignalingClient.Update;

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
    }

    public void Update()
    {
        playerBotController.Update();
        enemyBotController.Update();
        CheckDeath();
        GameSignalingUpdater();
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