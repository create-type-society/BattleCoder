using BattleCoder.Map;
using BattleCoder.Sound;
using UnityEngine;

namespace BattleCoder.StageTransition
{
    public class StageSelectButton : MonoBehaviour
    {
        [SerializeField] StageKind stageKind;
        [SerializeField] SoundManager soundManager;
        [SerializeField] AudioSource bgm;

        public async void OnClicked()
        {
            bgm.Stop();
            soundManager.MakeDecisionSound();
            await UniRx.Async.UniTask.Delay(500);
            SceneChangeManager.ChangePlayScene(stageKind);
        }
    }
}