﻿using UnityEngine;

namespace BattleCoder.Test.Tcp
{
    public class TcpTestComponent : MonoBehaviour
    {
        readonly MyTcpClient client = new MyTcpClient("localhost", 3000);

        MatchingServer matchingServer;
        int count = 0;

        void Start()
        {
            client.Connect();
            client.DisConnected += () => Debug.Log("TCP切断");
            matchingServer = new MatchingServer(client);
            matchingServer.Matched += () => { Debug.Log("マッチ"); };
        }

        void Update()
        {
            matchingServer.Update();
            count++;
            if (count % 60 == 0)
                client.WriteData("ほげ");
            if (count > 600)
                client.DisConnect();
        }
    }
}