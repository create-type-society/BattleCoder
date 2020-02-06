using System.Collections.Concurrent;

namespace BattleCoder.Tcp
{
//Tcpでやりとりするデータのキュー
    public class DataQueue
    {
        ConcurrentQueue<string> queue = new ConcurrentQueue<string>();

        public void EnQueue(string data)
        {
            queue.Enqueue(data);
        }

        public int Length()
        {
            return queue.Count;
        }

        public DeQueueResult DeQueue()
        {
            string str;
            return new DeQueueResult
            {
                isOk = queue.TryDequeue(out str),
                data = str
            };
        }
    }

    public struct DeQueueResult
    {
        public string data;
        public bool isOk;
    }
}