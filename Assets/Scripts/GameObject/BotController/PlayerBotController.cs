using System;
using BattleCoder.GamePlayUi;
using UnityEngine.Experimental.PlayerLoop;
using Object = UnityEngine.Object;

public class PlayerBotController
{
    readonly IUserInput userInput = new KeyController();
    readonly BotApplication botApplication;
    readonly PlayerHpPresenter playerHpPresenter;

    public PlayerBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
        CameraFollower cameraFollower, PlayerHpPresenter playerHpPresenter, RunButtonEvent runButtonEvent,
        ScriptText scriptText, ErrorMsg errorMsg, SoundManager soundManager)
    {
        this.playerHpPresenter = playerHpPresenter;
        var botEntity = Object.Instantiate(botEntityPrefab);
        cameraFollower.SetPlayerPosition(botEntity.transform);
        var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        botEntity.transform.position = tileMapInfo.GetPlayer1StartPosition();
        botApplication = new BotApplication(botEntity, botEntityAnimation, tileMapInfo, bulletPrefab, soundManager);
        userInput.ShootingAttackEvent += (sender, e) => { botApplication.Shot(); };

        var javaScriptEngine = new JavaScriptEngine(botApplication);
        runButtonEvent.AddClickEvent(() =>
        {
            try
            {
                javaScriptEngine.ExecuteJS(scriptText.GetScriptText());
            }
            catch (Exception e)
            {
                errorMsg.SetText(e.ToString());
            }
        });
    }

    public void Update()
    {
        botApplication.Update();
        userInput.Update();
        playerHpPresenter.RenderHp(botApplication.Hp);
    }

    public bool IsDeath()
    {
        return botApplication.Hp.IsDeath();
    }
}