using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class DeviceClientListener
{
    private const int ServerPort = 5700;

    private TcpListener _listener;
    private Task<TcpClient> _task;
    private TcpClient _client;

    public bool IsAccept { get; private set; }

    public DeviceClientListener()
    {
        _listener = TcpListener.Create(ServerPort);
        _listener.Start();
    }

    public void Close()
    {
        _client?.Close();
        _listener.Stop();
        Debug.Log("切断しました。");
    }

    public async void AcceptClient()
    {
        try
        {
            _task = _listener.AcceptTcpClientAsync();
            _client = await _task;
            IsAccept = true;
        }
        catch (ObjectDisposedException _)
        {
        }
    }

    public async Task<PacketData> ReadPacket()
    {
        try
        {
            var stream = _client.GetStream();
            var len = new byte[1];
            await stream.ReadAsync(len, 0, 1);
            if (len[0] >= 1)
            {
                byte[] buf = new byte[len[0]];
                await stream.ReadAsync(buf, 0, len[0]);

                return PacketData.CreatePacketData(buf);
            }
        }
        catch (InvalidOperationException e)
        {
            Debug.Log(e);
        }

        return new PacketData(PacketType.InputDeviceData, new byte[4]);
    }
}