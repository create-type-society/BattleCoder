using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // ボタンをクリックするとBattleSceneに移動します
    public void SingleButtonClicked()
    {
        SceneChangeManager.ChangeStageSelect();
    }
    
    public void MultiButtonClicked()
    {
        SceneChangeManager.ChangeMatchingScene();
    }
}