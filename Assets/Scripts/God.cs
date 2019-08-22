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

    BotEntityAnimation botEntityAnimation;

    BotApplication botApplication;

    JavaScriptEngine javaScriptEngine;

    void Awake()
    {
        var tileMapInfo = Instantiate(tileMapInfoManagerPrefab).Create(StageKind.TestStage);
        var botEntity = Instantiate(botEntityPrefab);
        cameraFollower.SetPlayerPosition(botEntity.transform);
        botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        botApplication = new BotApplication(botEntity, botEntityAnimation, tileMapInfo);
        javaScriptEngine = new JavaScriptEngine(botApplication);
        runButtonEvent.AddClickEvent(() => { javaScriptEngine.ExecuteJS(scriptText.GetScriptText()); });
    }

    void Update()
    {
        botApplication.Update();
        playerHpPresenter.RenderHp(botApplication.Hp);
    }
}