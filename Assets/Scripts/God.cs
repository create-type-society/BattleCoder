using UnityEngine;
using UnityEngine.Serialization;

//天地創造をする全てを支配する全知全能の神
public class God : MonoBehaviour
{
    [FormerlySerializedAs("botPrefab")] [SerializeField]
    BotEntity botEntityPrefab;

    BotEntityAnimation botEntityAnimation;

    BotApplication botApplication;
    JavaScriptEngine javaScriptEngine;
    int count = 0;

    void Awake()
    {
        var botEntity = Instantiate(botEntityPrefab);
        botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        botApplication = new BotApplication(botEntity, botEntityAnimation);
        javaScriptEngine = new JavaScriptEngine(botApplication);
    }

    void Update()
    {
        if (count % 60 == 0)
        {
            javaScriptEngine.ExecuteJS("Move(Math.floor(Math.random()*4),6,2)");
        }

        count++;
        botApplication.Update();
    }
}