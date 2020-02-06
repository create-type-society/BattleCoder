using System.Threading;
using BattleCoder.GameObject.BotApplication;
using BattleCoder.GameObject.BotApplication.BulletApplication.Bullet;
using BattleCoder.GamePlayUi;
using UnityEngine;

public class ClientBotController : IBotController
{
    readonly IUserInput userInput = new KeyController();
    readonly BotApplication botApplication;
    readonly PlayerHpPresenter playerHpPresenter;
    readonly JavaScriptEngine javaScriptEngine;
    readonly ErrorMsg errorMsg;

    public ClientBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
        CameraFollower cameraFollower, PlayerHpPresenter playerHpPresenter, RunButtonEvent runButtonEvent,
        ScriptText scriptText, ErrorMsg errorMsg, SoundManager soundManager, GameSignalingClient gameSignalingClient,
        MeleeAttackEntity meleeAttackEntity, ProcessScrollViewPresenter processScrollViewPresenter)
    {
        this.errorMsg = errorMsg;
        this.playerHpPresenter = playerHpPresenter;
        var botEntity = Object.Instantiate(botEntityPrefab);
        tileMapInfo.PlayerTankTransform = botEntity.transform;
        botEntity.gameObject.layer = LayerMask.NameToLayer("PlayerBot");
        cameraFollower.SetPlayerPosition(botEntity.transform);
        var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        botEntity.transform.position = tileMapInfo.GetPlayer2StartPosition();
        MeleeAttackApplication meleeAttackApplication = new MeleeAttackApplication(meleeAttackEntity, soundManager);
        var gun = new Gun(soundManager, new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("PlayerBullet")));

        botApplication = new BotApplication(
            botEntity, botEntityAnimation, tileMapInfo, gun,
            meleeAttackApplication, true
        );
        var hookBotApplication = new ClientBotCommandsHook(botApplication, gameSignalingClient);
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

    public Vector2 GetPos()
    {
        return botApplication.GetPos();
    }

    public void SetPos(Vector2 pos)
    {
        botApplication.SetPos(pos);
    }


    public void Dispose()
    {
        userInput.Dispose();
    }
}