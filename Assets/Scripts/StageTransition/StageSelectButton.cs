using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField] StageKind stageKind;

    public void OnClicked()
    {
        Debug.Log(stageKind);
    }
}