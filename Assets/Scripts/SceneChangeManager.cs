//Scene遷移処理を担当するクラス

using BattleCoder.Map;
using BattleCoder.Result;
using BattleCoder.StageTransition;
using BattleCoder.StartGameInfo;
using UnityEngine.SceneManagement;

namespace BattleCoder
{
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

        //シングルプレイでステージ選択画面へ移動する
        public static void ChangeSinglePlayStageSelect()
        {
            StartGameInfo.StartGameInfo.SetSinglePlay();
            SceneManager.LoadScene("StageSelectScene");
        }

        //マルチプレイでステージ選択画面へ移動する
        public static void ChangeMultiPlayStageSelect(MultiGameInfo multiGameInfo)
        {
            StartGameInfo.StartGameInfo.SetMultiPlay(multiGameInfo);
            SceneManager.LoadScene("StageSelectScene");
        }


        //プレイゲーム画面へ移動する
        public static void ChangePlayScene(StageKind stageKind)
        {
            SelectedStageData.SetSelectedStageKind(stageKind);
            SceneManager.LoadScene("SinglePlayGameScene");
        }

        //クライアント側がマルチプレイゲーム画面へ移動する
        public static void ChangeClientMultiPlayScene(StageKind stageKind, MultiGameInfo multiGameInfo)
        {
            SelectedStageData.SetSelectedStageKind(stageKind);
            StartGameInfo.StartGameInfo.SetMultiPlay(multiGameInfo);
            SceneManager.LoadScene("SinglePlayGameScene");
        }

        //タイトル画面へ遷移する
        public static void ChangeTitleScene()
        {
            SceneManager.LoadScene("Title");
        }
    }
}