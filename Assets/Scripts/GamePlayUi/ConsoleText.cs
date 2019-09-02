using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleText : MonoBehaviour
{
    [SerializeField] private Text consoleText;
    [SerializeField] private ScrollRect scrollRect;

    int lineCount = 0;
    const int maxLineCount = 200;


    public void AppendTextNewLine(string text)
    {
        AppendText(text + Environment.NewLine);
    }

    public void ClearText()
    {
        consoleText.text = "";
    }

    private void AppendText(string text)
    {
        consoleText.text += text;
        lineCount++;
        if (lineCount > maxLineCount)
        {
            lineCount = maxLineCount/2;
            consoleText.text = CutText(consoleText.text);
        }

        scrollRect.verticalNormalizedPosition = 0;
    }

    private string CutText(string text)
    {
        return
            string.Join(
                Environment.NewLine,
                text
                    .Split(new string[] {Environment.NewLine}, StringSplitOptions.None)
                    .Skip(maxLineCount / 2)
            );
    }
}