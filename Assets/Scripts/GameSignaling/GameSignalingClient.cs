using System;
using Newtonsoft.Json;
using UnityEngine;

//対戦する時のデータのやり取りをするクライアント側のクラス

public class GameSignalingClient : IDisposable
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
        while (true)
        {
            var result = myTcpClient.ReadData();
            if (result.isOk)
            {
                try
                {
                    ReceivedClientReceiveSignalData?.Invoke(
                        JsonConvert.DeserializeObject<ClientReceiveSignalData>(result.data)
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