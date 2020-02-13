using BattleCoder.Sound;
using UnityEngine;

namespace BattleCoder.Title
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] SoundManager soundManager;
        [SerializeField] AudioSource bgm;
        bool isClicked = false;

        // ボタンをクリックするとBattleSceneに移動します
        public async void SingleButtonClicked()
        {
            if (isClicked) return;
            isClicked = true;
            bgm.Stop();
            soundManager.MakeDecisionSound();
            await UniRx.Async.UniTask.Delay(500);
            SceneChangeManager.ChangeSinglePlayStageSelect();
        }

        public async void MultiButtonClicked()
        {
            if (isClicked) return;
            isClicked = true;
            bgm.Stop();
            soundManager.MakeDecisionSound();
            await UniRx.Async.UniTask.Delay(500);
            SceneChangeManager.ChangeMatchingScene();
        }
    }
}