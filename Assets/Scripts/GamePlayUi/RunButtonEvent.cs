using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class RunButtonEvent : MonoBehaviour
    {
        [SerializeField] Button runButton;

        public void AddClickEvent(UnityAction click)
        {
            runButton.onClick.AddListener(click);
        }
    }
}