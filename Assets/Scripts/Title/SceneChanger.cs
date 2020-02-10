using BattleCoder.Sound;
using UnityEngine;

namespace BattleCoder.Title
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] SoundManager soundManager;
        [SerializeField] AudioSource bgm;

        // ボタンをクリックするとBattleSceneに移動します
        public async void SingleButtonClicked()
        {
            bgm.Stop();
            soundManager.MakeDecisionSound();
            await UniRx.Async.UniTask.Delay(500);
            SceneChangeManager.ChangeSinglePlayStageSelect();
        }

        public async void MultiButtonClicked()
        {
            bgm.Stop();
            soundManager.MakeDecisionSound();
            await UniRx.Async.UniTask.Delay(500);
            SceneChangeManager.ChangeMatchingScene();
        }
    }
}