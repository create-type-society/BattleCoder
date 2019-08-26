using UnityEngine;
using System.Text;
using System.Net.Sockets;

namespace BattleCoder.Test.Tcp
{
    public class TcpTestComponent : MonoBehaviour
    {
        readonly string host = "localhost";
        readonly int port = 3000;
        TcpClient client;

        void Start()
        {
            client =
                new TcpClient(host, port);
            Debug.Log("接続");
            NetworkStream ns = client.GetStream();
            ns.ReadTimeout = 10000;
            ns.WriteTimeout = 10000;

            var enc = Encoding.UTF8;
            byte[] sendBytes = enc.GetBytes("abcdefg,hijklmn,ppp0055123123hkhkhkiiopQED" + '\n');

            ns.Write(sendBytes, 0, sendBytes.Length);

            var ms = new System.IO.MemoryStream();
            var resBytes = new byte[256];
            int resSize = 0;
            
            do
            {
                resSize = ns.Read(resBytes, 0, resBytes.Length);

                if (resSize == 0)
                {
                    Debug.Log("サーバーが切断しました。");
                    break;
                }

                ms.Write(resBytes, 0, resSize);
            } while (ns.DataAvailable || resBytes[resSize - 1] != '\n');

            string resMsg = enc.GetString(ms.GetBuffer(), 0, (int) ms.Length);
            ms.Close();

            resMsg = resMsg.TrimEnd('\n');
            Debug.Log(resMsg);

            ns.Close();
            client.Close();
            Debug.Log("切断しました。");
        }
    }
}