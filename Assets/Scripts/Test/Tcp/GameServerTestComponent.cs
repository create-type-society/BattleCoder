using BattleCoder.Common;
using BattleCoder.GameSignaling;
using BattleCoder.Map;
using BattleCoder.Matching;
using BattleCoder.Tcp;
using UnityEngine;

namespace BattleCoder.Test.Tcp
{
    public class GameServerTestComponent : MonoBehaviour
    {
        GameSignalingClient gameSignalingClient;
        GameSignalingHost gameSignalingHost;
        MatchingClient matchingServer1;
        MatchingClient matchingServer2;
        bool matchedHostFlag = false;
        bool matchedClientFlag = false;
        int count = 0;

        void Awake()
        {
            var myTcpClient1 = new MyTcpClient("localhost", 3000);
            var myTcpClient2 = new MyTcpClient("localhost", 3000);
            myTcpClient1.Connect();
            myTcpClient2.Connect();

            matchingServer1 = new MatchingClient(myTcpClient1, data =>
            {
                if (data.MatchingDataType == MatchingDataType.MatchedData)
                {
                    if (data.MatchType == MatchType.Host)
                        CreateGameSignaling(myTcpClient1, data.MatchType.Value);
                }
                else if (data.MatchingDataType == MatchingDataType.StageDeterminedData)
                    CreateGameSignaling(myTcpClient1, MatchType.Client);
            });
            matchingServer2 = new MatchingClient(myTcpClient2, data =>
            {
                if (data.MatchingDataType == MatchingDataType.MatchedData)
                {
                    if (data.MatchType == MatchType.Host)
                        CreateGameSignaling(myTcpClient2, data.MatchType.Value);
                }
                else if (data.MatchingDataType == MatchingDataType.StageDeterminedData)
                    CreateGameSignaling(myTcpClient2, MatchType.Client);
            });
        }

        void CreateGameSignaling(MyTcpClient myTcpClient, MatchType matchType)
        {
            if (MatchType.Client == matchType)
            {
                matchedClientFlag = true;
                gameSignalingClient = new GameSignalingClient(myTcpClient);
                gameSignalingClient.ReceivedClientReceiveSignalData += (obj) =>
                {
                    //   Debug.Log("ClientReceived:" + Enum.GetName(typeof(BattleResult), obj.battleResult));
                    //   Debug.Log("ClientReceived:" + Enum.GetName(typeof(CommandKind), obj.commandData.kind));
                };
            }
            else
            {
                matchedHostFlag = true;
                gameSignalingHost = new GameSignalingHost(myTcpClient, StageKind.Stage1);
                gameSignalingHost.ReceivedHostReceiveSignalData += (obj) =>
                {
                    //  Debug.Log("HostReceived:" + Enum.GetName(typeof(CommandKind), obj.clientCommandData.kind));
                };
            }
        }

        void Update()
        {
            if (matchedClientFlag && matchedHostFlag)
            {
                count++;
                gameSignalingHost.SendData(new ClientReceiveSignalData(
                        new CommandData(1, CommandKind.Move, count, new object[] {Direction.Down, 5}),
                        MatchType.Client
                    )
                );
                gameSignalingClient.SendData(new HostReceiveSignalData(
                    new CommandData(1, CommandKind.Move, count, new object[] {Direction.Up, 1}))
                );
                gameSignalingClient.SendData(new HostReceiveSignalData(
                    new CommandData(1, CommandKind.Move, count, new object[] {Direction.Up, 1}))
                );
                gameSignalingHost.SendData(new ClientReceiveSignalData(
                        new CommandData(1, CommandKind.Move, count, new object[] {Direction.Down, 5}),
                        MatchType.Client
                    )
                );
                gameSignalingClient.Update();
                gameSignalingHost.Update();
            }
            else
            {
                matchingServer1.Update();
                matchingServer2.Update();
            }
        }

        void OnDestroy()
        {
            gameSignalingClient.Dispose();
            gameSignalingHost.Dispose();
        }
    }
}