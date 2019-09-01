using System;
using UnityEngine;
using UnityEngine.UI;

public class ProcessPanelPresenter : MonoBehaviour
{
    [SerializeField] Button closeButton;

    public event Action closed;

    void Awake()
    {
        closeButton.onClick.AddListener(() => closed?.Invoke());
    }
}