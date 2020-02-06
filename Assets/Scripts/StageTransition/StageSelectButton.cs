using BattleCoder.Map;
using UnityEngine;

namespace BattleCoder.StageTransition
{
    public class StageSelectButton : MonoBehaviour
    {
        [SerializeField] StageKind stageKind;

        public void OnClicked()
        {
            SceneChangeManager.ChangePlayScene(stageKind);
        }
    }
}