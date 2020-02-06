using System;

namespace BattleCoder.Console
{
    public static class ConsoleLogger
    {
        public static event EventHandler<ConsoleLogEventArgs> ConsoleLogEvent;

        public static void Log(DateTime date, int processId, object obj)
        {
            OnConsoleLogEvent(null, new ConsoleLogEventArgs(date, processId, obj.ToString()));
        }

        private static void OnConsoleLogEvent(object sender, ConsoleLogEventArgs args)
        {
            ConsoleLogEvent?.Invoke(sender, args);
        }
    }
}