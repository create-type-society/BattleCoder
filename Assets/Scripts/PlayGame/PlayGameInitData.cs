using BattleCoder.GamePlayUi;

public struct PlayGameInitData
{
    public readonly BotEntity botEntityPrefab;
    public readonly CameraFollower cameraFollower;
    public readonly PlayerHpPresenter playerHpPresenter;
    public readonly TileMapInfo tileMapInfo;
    public readonly RunButtonEvent runButtonEvent;
    public readonly ScriptText scriptText;
    public readonly BulletEntity bulletPrefab;
    public readonly ErrorMsg errorMsg;
    public readonly SoundManager soundManager;
    public readonly MeleeAttackEntity meleeAttackPrefab;

    public PlayGameInitData(
        BotEntity botEntityPrefab,
        CameraFollower cameraFollower,
        PlayerHpPresenter playerHpPresenter,
        TileMapInfo tileMapInfo,
        RunButtonEvent runButtonEvent,
        ScriptText scriptText,
        BulletEntity bulletPrefab,
        ErrorMsg errorMsg,
        SoundManager soundManager,
        MeleeAttackEntity meleeAttackPrefab)
    {
        this.botEntityPrefab = botEntityPrefab;
        this.cameraFollower = cameraFollower;
        this.playerHpPresenter = playerHpPresenter;
        this.tileMapInfo = tileMapInfo;
        this.runButtonEvent = runButtonEvent;
        this.scriptText = scriptText;
        this.bulletPrefab = bulletPrefab;
        this.errorMsg = errorMsg;
        this.soundManager = soundManager;
        this.meleeAttackPrefab = meleeAttackPrefab;
    }
}