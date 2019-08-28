﻿//Scene遷移処理を担当するクラス

using System;
using UnityEngine.SceneManagement;

public static class SceneChangeManager
{
    //マッチング画面へ遷移する
    public static void ChangeMatchingScene()
    {
        SceneManager.LoadScene("MatchingScene");
    }
    
    //リザルト画面へ遷移する
    public static void ChangeResultScene(bool result)
    {
        ResultInfo.SetResult(result);
        SceneManager.LoadScene("ResultScene");
    }

    //ステージ選択画面へ移動する
    public static void ChangeStageSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    //ゲーム画面へ移動する
    public static void ChangePlayScene(StageKind stageKind)
    {
        SelectedStageData.SetSelectedStageKind(stageKind);
        SceneManager.LoadScene("SinglePlayGameScene");
    }

    //タイトル画面へ遷移する
    public static void ChangeTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}