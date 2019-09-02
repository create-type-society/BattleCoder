using System.Threading;
using BattleCoder.GameObject.BotApplication.BulletApplication.Bullet;
using BattleCoder.GamePlayUi;
using UnityEngine;

public class PlayerBotController : IBotController
{
    readonly IUserInput userInput = new DeviceController();
    readonly BotApplication botApplication;
    readonly PlayerHpPresenter playerHpPresenter;
    readonly JavaScriptEngine javaScriptEngine;
    readonly ErrorMsg errorMsg;

    public PlayerBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
        CameraFollower cameraFollower, PlayerHpPresenter playerHpPresenter, RunButtonEvent runButtonEvent,
        ScriptText scriptText, ErrorMsg errorMsg, SoundManager soundManager, MeleeAttackEntity meleeAttackEntity,
        ProcessScrollViewPresenter processScrollViewPresenter)
    {
        this.errorMsg = errorMsg;
        this.playerHpPresenter = playerHpPresenter;
        var botEntity = Object.Instantiate(botEntityPrefab);
        tileMapInfo.PlayerTankTransform = botEntity.transform;
        botEntity.gameObject.layer = LayerMask.NameToLayer("PlayerBot");
        cameraFollower.SetPlayerPosition(botEntity.transform);
        var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        botEntity.transform.position = tileMapInfo.GetPlayer1StartPosition();
        MeleeAttackApplication meleeAttackApplication = new MeleeAttackApplication(meleeAttackEntity, soundManager);
        botApplication = new BotApplication(
            botEntity, botEntityAnimation, tileMapInfo,
            new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("PlayerBullet")),
            soundManager, meleeAttackApplication
        );
        userInput.ShootingAttackEvent += (sender, e) => { botApplication.Shot(); };
        userInput.MeleeAttackEvent += (sender, e) => { botApplication.MeleeAttack(); };

        javaScriptEngine = new JavaScriptEngine(botApplication);
        runButtonEvent.AddClickEvent(async () =>
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var panel =
                processScrollViewPresenter.AddProcessPanel(
                    () => { tokenSource.Cancel(); });
            var task = javaScriptEngine.ExecuteJS(scriptText.GetScriptText(), token, panel.ProcessId);

            await task;
            panel.Dispose();
        });
    }

    public void Update()
    {
        userInput.Update();
        botApplication.Update();
        playerHpPresenter.RenderHp(botApplication.Hp);
        var errorText = javaScriptEngine.GetErrorText();
        if (errorText != "")
            errorMsg.SetText(errorText);
    }
    
    public Vector2 GetPos()
    {
        return botApplication.GetPos();
    }

    public void SetPos(Vector2 pos)
    {
        return;
        botApplication.SetPos(pos);
    }


    public bool IsDeath()
    {
        return botApplication.Hp.IsDeath();
    }

    public void Dispose()
    {
        userInput.Dispose();
    }
}