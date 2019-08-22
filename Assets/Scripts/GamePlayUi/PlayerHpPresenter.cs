using UnityEngine;
using UnityEngine.UI;

//プレイヤーHpの描画を制御する
public class PlayerHpPresenter : MonoBehaviour
{
    [SerializeField] Text text;

    //Hpを描画する
    public void RenderHp(BotHp botHp)
    {
        text.text = "HP:" + botHp.hp;
    }
}