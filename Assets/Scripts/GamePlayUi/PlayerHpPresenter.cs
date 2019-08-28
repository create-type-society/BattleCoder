using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//プレイヤーHpの描画を制御する
public class PlayerHpPresenter : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    Stack<GameObject> heartObjects = new Stack<GameObject>();
    Vector3 latestHeartPosition = new Vector3(-590, 440);
    Vector3 heartPosition = new Vector3(62, 0);

    //Hpを描画する
    public void RenderHp(BotHp botHp)
    {
        if (heartObjects.Count > botHp.hp)
        {
            GameObject disHeart = heartObjects.Pop();
            Destroy(disHeart);
        }
        else if (heartObjects.Count < botHp.hp)
        {
            GameObject incHeart = Instantiate(heartPrefab);
            RectTransform rectTransform = incHeart.GetComponent<RectTransform>();
            rectTransform.SetParent(GetComponent<RectTransform>(), false);
            rectTransform.anchoredPosition = latestHeartPosition;
            latestHeartPosition += heartPosition;
            heartObjects.Push(incHeart);
        }
    }
}