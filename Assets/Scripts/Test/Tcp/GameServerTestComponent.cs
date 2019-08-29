using System;
using UnityEngine;

namespace BattleCoder.Test.Tcp
{
    public class GameServerTestComponent : MonoBehaviour
    {
        GameSignalingClient gameSignalingClient;
        GameSignalingHost gameSignalingHost;
        MatchingServer matchingServer1;
        MatchingServer matchingServer2;
        bool matchedHostFlag = false;
        bool matchedClientFlag = false;

        int count = 0;

        void Awake()
        {
            var myTcpClient1 = new MyTcpClient("localhost", 3000);
            var myTcpClient2 = new MyTcpClient("localhost", 3000);
            myTcpClient1.Connect();
            myTcpClient2.Connect();

            matchingServer1 = new MatchingServer(myTcpClient1);
            matchingServer2 = new MatchingServer(myTcpClient2);

            matchingServer1.Matched += (matchType) => { CreateGameSignaling(myTcpClient1, matchType); };
            matchingServer2.Matched += (matchType) => { CreateGameSignaling(myTcpClient2, matchType); };
        }

        void CreateGameSignaling(MyTcpClient myTcpClient, MatchType matchType)
        {
            if (MatchType.Client == matchType)
            {
                matchedClientFlag = true;
                gameSignalingClient = new GameSignalingClient(myTcpClient);
                gameSignalingClient.ReceivedClientReceiveSignalData += (obj) =>
                {
                    Debug.Log("ClientReceived:" + Enum.GetName(typeof(BattleResult), obj.battleResult));
                    Debug.Log("ClientReceived:" + Enum.GetName(typeof(CommandKind), obj.commandData.kind));
                };
            }
            else
            {
                matchedHostFlag = true;
                gameSignalingHost = new GameSignalingHost(myTcpClient);
                gameSignalingHost.ReceivedHostReceiveSignalData += (obj) =>
                {
                    Debug.Log("HostReceived:" + Enum.GetName(typeof(CommandKind), obj.commandData.kind));
                };
            }
        }

        void Update()
        {
            if (matchedClientFlag && matchedHostFlag)
            {
                gameSignalingClient.Update();
                gameSignalingHost.Update();

                count++;
                if (count % 60 == 0)
                    gameSignalingClient.SendData(new HostReceiveSignalData(
                        new CommandData(CommandKind.Move, new object[] {Direction.Up, 1}))
                    );
                if (count % 120 == 0)
                    gameSignalingHost.SendData(new ClientReceiveSignalData(
                        BattleResult.Wait,
                        new CommandData(CommandKind.Move, new object[] {Direction.Down, 5}))
                    );
            }
            else
            {
                matchingServer1.Update();
                matchingServer2.Update();
            }
        }
    }
}