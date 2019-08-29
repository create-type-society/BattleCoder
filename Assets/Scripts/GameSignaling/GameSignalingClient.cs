using System;
using Newtonsoft.Json;
using UnityEngine;

//対戦する時のデータのやり取りをするクライアント側のクラス

public class GameSignalingClient
{
    readonly MyTcpClient myTcpClient;
    public event Action<ClientReceiveSignalData> ReceivedClientReceiveSignalData;

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
        var result = myTcpClient.ReadData();
        if (result.isOk)
        {
            
            ReceivedClientReceiveSignalData?.Invoke(
                JsonConvert.DeserializeObject<ClientReceiveSignalData>(result.data)
            );
        }
    }
}