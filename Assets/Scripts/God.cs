using BattleCoder.GamePlayUi;
using UnityEngine;
using UnityEngine.Serialization;

//天地創造をする全てを支配する全知全能の神
public class God : MonoBehaviour
{
    [FormerlySerializedAs("botPrefab")] [SerializeField]
    BotEntity botEntityPrefab;

    [SerializeField] TileMapInfoManager tileMapInfoManagerPrefab;
    [SerializeField] CameraFollower cameraFollower;
    [SerializeField] PlayerHpPresenter playerHpPresenter;

    [SerializeField] RunButtonEvent runButtonEvent;
    [SerializeField] ScriptText scriptText;
    [SerializeField] BulletEntity bulletPrefab;
    [SerializeField] ErrorMsg errorMsg;
    [SerializeField] SoundManager soundManager;
    [SerializeField] MeleeAttackEntity meleeAttackPrefab;
    [SerializeField] private ConsoleWindow consoleWindow;

    IPlayGame playGame;

    void Awake()
    {
        if (StartGameInfo.IsSinglePlay == false)
            playGame = MultiPlayGameFactory.CreateMultiPlayGame(GetPlayGameInitData());
        else
            playGame = new SinglePlayGame(GetPlayGameInitData());
    }

    void Update()
    {
        playGame.Update();
    }

    PlayGameInitData GetPlayGameInitData()
    {
        var tileMapInfo = Instantiate(tileMapInfoManagerPrefab).Create(SelectedStageData.GetSelectedStageKind());

        return new PlayGameInitData(
            botEntityPrefab, cameraFollower,
            playerHpPresenter, tileMapInfo,
            runButtonEvent, scriptText, bulletPrefab,
            errorMsg, soundManager, meleeAttackPrefab
        );
    }
}