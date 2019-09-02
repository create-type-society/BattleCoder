using System;
using UnityEngine;

public class ProcessScrollViewPresenter : MonoBehaviour
{
    [SerializeField] GameObject content;

    [SerializeField] ProcessPanelPresenter processPanelPrefab;


    public IDisposable AddProcessPanel(Action processClosedCallback)
    {
        var obj = Instantiate(processPanelPrefab, content.transform);
        obj.closed += () =>
        {
            processClosedCallback();
            Destroy(obj.gameObject);
        };
        return obj;
    }
}