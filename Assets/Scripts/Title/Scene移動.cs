using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene移動 : MonoBehaviour
{

    // ボタンをクリックするとBattleSceneに移動します
    public void ButtonClicked()
    {
        SceneManager.LoadScene("SinglePlayGameScene");
    }
}