using UnityEngine;

public class ProcessScrollViewPresenter : MonoBehaviour
{
    [SerializeField] GameObject content;

    [SerializeField] ProcessPanelPresenter processPanelPrefab;

    void Start()
    {
        for (var i = 0; i < 10; i++)
            Instantiate(processPanelPrefab, content.transform);
    }
}