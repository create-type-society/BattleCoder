using System;
using Newtonsoft.Json;

public class GameSignalingHost
{
    readonly MyTcpClient myTcpClient;
    public event Action<HostReceiveSignalData> ReceivedHostReceiveSignalData;

    public GameSignalingHost(MyTcpClient myTcpClient)
    {
        this.myTcpClient = myTcpClient;
    }

    public void SendData(ClientReceiveSignalData data)
    {
        myTcpClient.WriteData(JsonConvert.SerializeObject(data));
    }

    public void Update()
    {
        var result = myTcpClient.ReadData();
        if (result.isOk)
            ReceivedHostReceiveSignalData?.Invoke(
                JsonConvert.DeserializeObject<HostReceiveSignalData>(result.data)
            );
    }
}