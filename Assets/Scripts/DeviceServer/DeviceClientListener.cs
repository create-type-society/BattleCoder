using System;
using System.Net.Sockets;
using System.Threading.Tasks;

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
        _listener.Stop();
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
}