using UnityEngine;
using UnityEngine.Serialization;

//天地創造をする全てを支配する全知全能の神
public class God : MonoBehaviour
{
    [FormerlySerializedAs("botPrefab")] [SerializeField]
    BotEntity botEntityPrefab;

    BotApplication botApplication;

    void Awake()
    {
        var botEntity = Instantiate(botEntityPrefab);
        botApplication = new BotApplication(botEntity);
        botApplication.Move(Direction.Right, 5, 3);
    }

    void Update()
    {
        botApplication.Update();
    }
}