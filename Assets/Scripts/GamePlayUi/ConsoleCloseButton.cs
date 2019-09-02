using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ConsoleCloseButton : MonoBehaviour
{
    [SerializeField] private Button button;

    public void AddListener(UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}