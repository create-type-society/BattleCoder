using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleText : MonoBehaviour
{
    [SerializeField] private Text consoleText;
    [SerializeField] private ScrollRect scrollRect;

    public void AppendText(string text)
    {
        consoleText.text += text;
        scrollRect.verticalNormalizedPosition = 0;
    }

    public void AppendTextNewLine(string text)
    {
        AppendText(text + Environment.NewLine);
    }

    public void ClearText()
    {
        consoleText.text = "";
    }
}