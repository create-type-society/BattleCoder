using System;
using Newtonsoft.Json;
using UnityEngine;

//対戦する時のデータのやり取りをするクライアント側のクラス

public class GameSignalingClient : IDisposable
{
    readonly MyTcpClient myTcpClient;
    public event Action<ClientReceiveSignalData> ReceivedClientReceiveSignalData;
    public event Action<BattleResult> ReceivedBattleResult;
    public event Action<Vector2> ReceivedHostPos;
    public event Action<Vector2> ReceivedClientPos;

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
        else if (s.IndexOf("client_pos") == 0)
        {
            var ss = s.Split(',');
            ReceivedClientPos?.Invoke(new Vector2(float.Parse(ss[1]), float.Parse(ss[2])));
        }
        else if (s.IndexOf("host_pos") == 0)
        {
            var ss = s.Split(',');
            ReceivedClientPos?.Invoke(new Vector2(float.Parse(ss[1]), float.Parse(ss[2])));
        }
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