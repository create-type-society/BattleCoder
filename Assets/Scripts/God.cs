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


    PlayerBotController playerBotController;
    CpuBotController cpuBotController;
    JavaScriptEngine javaScriptEngine;

    void Awake()
    {
        var tileMapInfo = Instantiate(tileMapInfoManagerPrefab).Create(SelectedStageData.GetSelectedStageKind());
        playerBotController = new PlayerBotController(botEntityPrefab, tileMapInfo, bulletPrefab, cameraFollower,
            playerHpPresenter, runButtonEvent, scriptText, errorMsg, soundManager, Instantiate(meleeAttackPrefab));
        cpuBotController = new CpuBotController(botEntityPrefab, tileMapInfo, bulletPrefab, soundManager, Instantiate(meleeAttackPrefab));
    }

    void Update()
    {
        playerBotController.Update();
        cpuBotController.Update();
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (playerBotController.IsDeath())
        {
            SceneChangeManager.ChangeResultScene(false);
        }

        if (cpuBotController.ISDeath())
        {
            SceneChangeManager.ChangeResultScene(true);
        }
    }
}