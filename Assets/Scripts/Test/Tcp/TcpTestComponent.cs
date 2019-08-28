using UnityEngine;

namespace BattleCoder.Test.Tcp
{
    public class TcpTestComponent : MonoBehaviour
    {
        readonly MyTcpClient client = new MyTcpClient("localhost", 3000);
        int count = 0;

        void Start()
        {
            client.Connect();
            client.DisConnected += () => Debug.Log("TCP切断");
        }

        void Update()
        {
            count++;
            if (count % 60 == 0)
                client.WriteData("ほげ");
            var result = client.ReadData();
            if (result.isOk)
                Debug.Log(result.data);
        }
    }
}