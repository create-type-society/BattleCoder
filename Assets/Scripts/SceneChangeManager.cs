//Scene遷移処理を担当するクラス

using System;
using UnityEngine.SceneManagement;

public static class SceneChangeManager
{
    //リザルト画面へ遷移する
    public static void ChangeResultScene(bool result)
    {
        ResultInfo.SetResult(result);
        SceneManager.LoadScene("ResultScene");
    }

    //ステージ選択画面へ移動する
    public static void ChangeStageSelect()
    {
        throw new NotImplementedException();
    }

    //ゲーム画面へ移動する
    public static void ChangePlayScene()
    {
        throw new NotImplementedException();
    }

    //タイトル画面へ遷移する
    public static void ChangeTitleScene()
    {
        throw new NotImplementedException();
    }
}