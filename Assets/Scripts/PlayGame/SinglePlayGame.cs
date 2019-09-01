using UnityEngine;

public class SinglePlayGame : IPlayGame
{
    readonly IBotController playerBotController;
    readonly IBotController enemyBotController;

    public SinglePlayGame(PlayGameInitData playGameInitData)
    {
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

        enemyBotController = new CpuBotController(
            playGameInitData.botEntityPrefab,
            playGameInitData.tileMapInfo,
            playGameInitData.bulletPrefab,
            playGameInitData.soundManager,
            Object.Instantiate(playGameInitData.meleeAttackPrefab)
        );
    }

    public void Update()
    {
        playerBotController.Update();
        enemyBotController.Update();
        CheckDeath();
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