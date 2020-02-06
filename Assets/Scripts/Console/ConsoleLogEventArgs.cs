using System;

namespace BattleCoder.Console
{
    public class ConsoleLogEventArgs : EventArgs
    {
        public DateTime Date { get; }
        public int ProcessId { get; }
        public string Msg { get; }

        public ConsoleLogEventArgs(DateTime date, int processId, string msg)
        {
            Date = date;
            ProcessId = processId;
            Msg = msg;
        }
    }
}