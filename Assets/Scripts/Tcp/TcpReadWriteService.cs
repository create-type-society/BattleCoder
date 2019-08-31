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

        public Task CreateReadWriteTask(NetworkStream ns, DataQueue readDataQueue, DataQueue writeDataQueue)
        {
            var sr = new StreamReader(ns, enc);
            return Task.WhenAll(Read(sr, readDataQueue), Write(ns, writeDataQueue));
        }

        Task Read(StreamReader sr, DataQueue readDataQueue)
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    var resMsg = sr.ReadLine().TrimEnd('\n');
                    readDataQueue.EnQueue(resMsg);
                }
            });
        }

        Task Write(NetworkStream ns, DataQueue writeDataQueue)
        {
            return Task.Run(() =>
            {
                var count = 0;
                while (true)
                {
                    var result = writeDataQueue.DeQueue();
                    if (result.isOk == false)
                    {
                        count++;
                        if (count != 10000) continue;
                        WriteEmpty(ns);
                        count = 0;
                        continue;
                    }

                    count = 0;
                    var sendBytes = enc.GetBytes(result.data);
                    ns.Write(sendBytes, 0, sendBytes.Length);
                }
            });
        }

        void WriteEmpty(NetworkStream ns)
        {
            var sendBytes = enc.GetBytes("empty\n");
            ns.Write(sendBytes, 0, sendBytes.Length);
        }
    }
}