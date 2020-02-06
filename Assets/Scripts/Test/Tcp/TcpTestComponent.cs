using BattleCoder.Matching;
using BattleCoder.Tcp;
using UnityEngine;

namespace BattleCoder.Test.Tcp
{
    public class TcpTestComponent : MonoBehaviour
    {
        readonly MyTcpClient client = new MyTcpClient("192.168.179.4", 3000);

        MatchingClient matchingClient;
        int count = 0;

        void Start()
        {
            client.Connect();
            client.DisConnected += () => Debug.Log("TCP切断");
            matchingClient = new MatchingClient(client);
            matchingClient.Matched += (matchType) =>
            {
                Debug.Log("マッチ");
                Debug.Log(matchType);
            };
        }

        void Update()
        {
            matchingClient.Update();
            count++;
            if (count % 60 == 0)
                client.WriteData("ほげ");
            if (count > 600)
                client.DisConnect();
        }
    }
}