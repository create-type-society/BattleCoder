using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BattleCoder.Tcp
{
    public class TcpReadWriteService
    {
        readonly Encoding enc = Encoding.UTF8;
        readonly byte[] resBytes = new byte[4 * 1024];

        public Task CreateReadWriteTask(NetworkStream ns, DataQueue readDataQueue, DataQueue writeDataQueue)
        {
            return Task.Run(() =>
            {
                var sr = new StreamReader(ns, enc);
                while (true)
                {
                    WriteEmpty(ns);
                    Write(ns, writeDataQueue);
                    Read(sr, readDataQueue);
                }
            });
        }

        public void Read(StreamReader sr, DataQueue readDataQueue)
        {
            var resMsg = sr.ReadLine().TrimEnd('\n');
            if (resMsg != "empty")
                readDataQueue.EnQueue(resMsg);
        }

        public void Write(NetworkStream ns, DataQueue writeDataQueue)
        {
            var result = writeDataQueue.DeQueue();
            if (result.isOk == false) return;
            var sendBytes = enc.GetBytes(result.data);
            ns.Write(sendBytes, 0, sendBytes.Length);
        }

        public void WriteEmpty(NetworkStream ns)
        {
            var sendBytes = enc.GetBytes("empty\n");
            ns.Write(sendBytes, 0, sendBytes.Length);
        }
    }
}