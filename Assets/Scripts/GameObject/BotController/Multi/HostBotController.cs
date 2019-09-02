using System.Threading;
using BattleCoder.GameObject.BotApplication.BulletApplication.Bullet;
using BattleCoder.GamePlayUi;
using UnityEngine;

public class HostBotController : IBotController
{
    readonly IUserInput userInput = new DeviceController();
    readonly BotApplication botApplication;
    readonly PlayerHpPresenter playerHpPresenter;
    readonly JavaScriptEngine javaScriptEngine;
    readonly ErrorMsg errorMsg;

    public HostBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
        CameraFollower cameraFollower, PlayerHpPresenter playerHpPresenter, RunButtonEvent runButtonEvent,
        ScriptText scriptText, ErrorMsg errorMsg, SoundManager soundManager, GameSignalingHost gameSignalingHost,
        MeleeAttackEntity meleeAttackEntity, ProcessScrollViewPresenter processScrollViewPresenter)
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
        botApplication = new BotApplication(botEntity, botEntityAnimation, tileMapInfo,
            new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("PlayerBullet")), soundManager,
            meleeAttackApplication);
        var hookBotApplication = new HostBotCommandsHook(botApplication, gameSignalingHost);
        userInput.ShootingAttackEvent += (sender, e) => { hookBotApplication.Shot(); };
        userInput.MeleeAttackEvent += (sender, e) => { hookBotApplication.MeleeAttack(); };

        javaScriptEngine = new JavaScriptEngine(hookBotApplication);
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
        botApplication.Update();
        userInput.Update();
        playerHpPresenter.RenderHp(botApplication.Hp);
        var errorText = javaScriptEngine.GetErrorText();
        if (errorText != "")
            errorMsg.SetText(errorText);
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