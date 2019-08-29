using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.GamePlayUi
{
    public class ConsoleText : MonoBehaviour
    {
        [SerializeField] private Text consoleText;

        public void AppendText(string text)
        {
            consoleText.text += text;
        }

        public void AppendTextNewLine(string text)
        {
            consoleText.text = text + Environment.NewLine;
        }

        public void ClearText()
        {
            consoleText.text = "";
        }
    }
}