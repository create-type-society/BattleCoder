using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RunButtonEvent : MonoBehaviour
{
    [SerializeField] Button runButton;

    public void AddClickEvent(UnityAction click)
    {
        runButton.onClick.AddListener(click);
    }
}