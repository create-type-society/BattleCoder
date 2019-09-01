using System;
using Newtonsoft.Json;
using UnityEngine;

//対戦する時のデータのやり取りをするクライアント側のクラス

public class GameSignalingClient : IDisposable
{
    readonly MyTcpClient myTcpClient;
    public event Action<ClientReceiveSignalData> ReceivedClientReceiveSignalData;
    public event Action<BattleResult> ReceivedBattleResult;

    public GameSignalingClient(MyTcpClient myTcpClient)
    {
        this.myTcpClient = myTcpClient;
    }

    public void SendData(HostReceiveSignalData data)
    {
        myTcpClient.WriteData(JsonConvert.SerializeObject(data));
    }

    public void Update()
    {
        while (true)
        {
            var result = myTcpClient.ReadData();
            if (result.isOk)
                Receive(result.data);
            else return;
        }
    }

    void Receive(string s)
    {
        if (s == BattleResult.YouLose.ToString())
            ReceivedBattleResult?.Invoke(BattleResult.YouLose);
        else if (s == BattleResult.YouWin.ToString())
            ReceivedBattleResult?.Invoke(BattleResult.YouWin);
        else
        {
            try
            {
                ReceivedClientReceiveSignalData?.Invoke(
                    JsonConvert.DeserializeObject<ClientReceiveSignalData>(s)
                );
            }
            catch (Exception e)
            {
                Debug.Log("Raw Json String " + s);
                throw e;
            }
        }
    }

    public void Dispose()
    {
        myTcpClient.DisConnect();
    }
}