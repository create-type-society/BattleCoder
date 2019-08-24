using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField] StageKind stageKind;

    public void OnClicked()
    {
        SceneChangeManager.ChangePlayScene(stageKind);
    }
}