using System;
using UniRx.Triggers;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class CommandPallet : MonoBehaviour
    {
        [SerializeField] InputField inputField;
        int caretPosition = 0;

        void Start()
        {
            foreach (var x in GetComponentsInChildren<CommandButton>())
            {
                x.SetClickedCallback(AddText);
            }
        }

        void Update()
        {
            if (inputField.isFocused)
                caretPosition = inputField.caretPosition;
        }

        async void AddText(string newText)
        {
            var text = inputField.text;
            if (text.Length < caretPosition) caretPosition = inputField.caretPosition;
            inputField.text = text.Insert(caretPosition, newText);
            var tmpCaretPosition = caretPosition + newText.Length;
            inputField.Select();
            await UniTask.DelayFrame(0);
            inputField.selectionFocusPosition = tmpCaretPosition;
            inputField.selectionAnchorPosition = tmpCaretPosition;
            inputField.caretPosition = tmpCaretPosition;
        }
    }
}