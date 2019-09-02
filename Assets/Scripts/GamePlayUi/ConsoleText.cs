using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleText : MonoBehaviour
{
    [SerializeField] private Text consoleText;

    public void AppendText(string text)
    {
        consoleText.text += text;
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