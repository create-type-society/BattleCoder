using System;
using BattleCoder.Map;
using BattleCoder.Tcp;
using Newtonsoft.Json;
using UnityEngine;

namespace BattleCoder.GameSignaling
{
    public class GameSignalingHost : IDisposable
    {
        readonly MyTcpClient myTcpClient;
        public event Action<HostReceiveSignalData> ReceivedHostReceiveSignalData;

        public GameSignalingHost(MyTcpClient myTcpClient, StageKind stageKind)
        {
            this.myTcpClient = myTcpClient;
            myTcpClient.WriteData("stage_kind:" + (int) stageKind);
        }

        public void SendData(ClientReceiveSignalData data)
        {
            myTcpClient.WriteData(JsonConvert.SerializeObject(data));
        }

        public void SendBattleResult(BattleResult battleResult)
        {
            myTcpClient.WriteData(battleResult.ToString());
        }

        public void SendClientPos(Vector3 pos)
        {
            myTcpClient.WriteData($"client_pos,{pos.x},{pos.y}");
        }

        public void SendHostPos(Vector3 pos)
        {
            myTcpClient.WriteData($"host_pos,{pos.x},{pos.y}");
        }


        public void Update()
        {
            while (true)
            {
                var result = myTcpClient.ReadData();
                if (result.isOk)
                {
                    try
                    {
                        ReceivedHostReceiveSignalData?.Invoke(
                            JsonConvert.DeserializeObject<HostReceiveSignalData>(result.data)
                        );
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Raw Json String " + result.data);
                        throw e;
                    }
                }
                else return;
            }
        }

        public void Dispose()
        {
            myTcpClient.DisConnect();
        }
    }
}