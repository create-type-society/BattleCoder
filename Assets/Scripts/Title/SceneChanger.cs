using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // ボタンをクリックするとBattleSceneに移動します
    public void SingleButtonClicked()
    {
        SceneChangeManager.ChangeSinglePlayStageSelect();
    }

    public void MultiButtonClicked()
    {
        SceneChangeManager.ChangeMatchingScene();
    }
}