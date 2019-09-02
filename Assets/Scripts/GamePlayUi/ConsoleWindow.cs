using System.Collections.Concurrent;
using UnityEngine;

public class ConsoleWindow : MonoBehaviour
{
    [SerializeField] private ConsoleText consoleText;
    [SerializeField] private ConsoleClearButton clearButton;

    public ConcurrentQueue<string> syncedQueue = new ConcurrentQueue<string>();

    private void Start()
    {
        clearButton.AddListener(() => consoleText.ClearText());

        ConsoleLogger.ConsoleLogEvent += OnConsoleLogEvent;
    }

    private void Update()
    {
        if (!syncedQueue.IsEmpty)
        {
            syncedQueue.TryDequeue(out string msg);
            consoleText.AppendTextNewLine(msg);
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnConsoleLogEvent(object sender, ConsoleLogEventArgs e)
    {
        syncedQueue.Enqueue($"[{e.Date}][Process {e.ProcessId}] {e.Msg}");
    }
}