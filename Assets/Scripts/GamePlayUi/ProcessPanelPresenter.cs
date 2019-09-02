using System;
using UnityEngine;
using UnityEngine.UI;

public class ProcessPanelPresenter : MonoBehaviour, IDisposable
{
    [SerializeField] Button closeButton;
    [SerializeField] Text text;

    static int count = 0;
    public event Action closed;
    bool closedFlag = false;

    void Awake()
    {
        count++;
        text.text = "プロセス:" + count;
        closeButton.onClick.AddListener(() => closed?.Invoke());
    }

    void Update()
    {
        if (closedFlag)
        {
            closed = null;
            Destroy(gameObject);
        }
    }


    public void Dispose()
    {
        closedFlag = true;
    }
}