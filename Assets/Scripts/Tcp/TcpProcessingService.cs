using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BattleCoder.Tcp
{
    public static class TcpProcessingService
    {
        public static Task CreateReceiveTask(NetworkStream ns, DataQueue readDataQueue)
        {
            var enc = Encoding.UTF8;
            var resBytes = new byte[4 * 1024];
            int resSize = 0;
            return Task.Run(() =>
            {
                while (true)
                {
                    var ms = new System.IO.MemoryStream();
                    do
                    {
                        resSize = ns.Read(resBytes, 0, resBytes.Length);
                        if (resSize == 0)
                            return;

                        ms.Write(resBytes, 0, resSize);
                    } while (ns.DataAvailable || resBytes[resSize - 1] != '\0');

                    string resMsg = enc.GetString(ms.GetBuffer(), 0, (int) ms.Length);
                    ms.Close();

                    resMsg = resMsg.TrimEnd('\0');
                    if (resMsg != "empty")
                        readDataQueue.EnQueue(resMsg);
                }
            });
        }

        public static Task CreateWriteTask(NetworkStream ns, DataQueue writeDataQueue)
        {
            var enc = Encoding.UTF8;
            return Task.Run(() =>
                {
                    while (true)
                    {
                        var result = writeDataQueue.DeQueue();
                        string data = "empty\0";
                        if (result.isOk)
                            data = result.data;
                        var sendBytes = enc.GetBytes(data + '\n');
                        ns.Write(sendBytes, 0, sendBytes.Length);
                        Thread.Sleep(10);
                    }
                }
            );
        }
    }
}