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
        enemyBotController = new RemoteClientBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.soundManager,
            gameSignalingHost,
            Object.Instantiate(playGameInitData.meleeAttackPrefab)
        );
    }

    public void Update()
    {
        playerBotController.Update();
        enemyBotController.Update();
        CheckDeath();
        gameSignalingHost.Update();
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
    }
}