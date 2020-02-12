using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class CommandButton : MonoBehaviour
    {
        Action<string> callback;

        [SerializeField] string text;

        void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable().Subscribe((_) => OnClick());
        }

        public void SetClickedCallback(Action<string> f)
        {
            callback = f;
        }

        public void OnClick()
        {
            callback(text);
        }
    }
}