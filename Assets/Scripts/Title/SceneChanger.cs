using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // ボタンをクリックするとBattleSceneに移動します
    public void SingleButtonClicked()
    {
        SceneChangeManager.ChangeStageSelect();
    }
    
    public void MultiButtonClicked()
    {
        SceneChangeManager.ChangeMatchingScene();
    }
}