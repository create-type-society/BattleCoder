using System.Collections.Concurrent;
using UnityEngine;

public class ConsoleWindow : MonoBehaviour
{
    [SerializeField] private ConsoleText consoleText;
    [SerializeField] private ConsoleClearButton clearButton;
    [SerializeField] private ConsoleCloseButton closeButton;

    public ConcurrentQueue<string> syncedQueue = new ConcurrentQueue<string>();

    public ConsoleWindow()
    {
        ConsoleLogger.ConsoleLogEvent += OnConsoleLogEvent;
    }

    private void Start()
    {
        clearButton.AddListener(() => consoleText.ClearText());
        closeButton.AddListener(Close);
    }

    private void Update()
    {
        if (!syncedQueue.IsEmpty && gameObject.activeSelf)
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