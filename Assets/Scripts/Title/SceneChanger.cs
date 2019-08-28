﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    // ボタンをクリックするとBattleSceneに移動します
    public void SingleButtonClicked()
    {
        SceneManager.LoadScene("SinglePlayGameScene");
    }
    
    public void MultiButtonClicked()
    {
        SceneManager.LoadScene("MatchingScene");
    }
}