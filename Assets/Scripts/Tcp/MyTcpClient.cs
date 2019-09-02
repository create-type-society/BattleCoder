//Tcp通信をするクラス

using System;
using System.Net.Sockets;
using System.Threading;
using BattleCoder.Tcp;

public class MyTcpClient
{
    readonly string host;
    readonly int port;
    TcpClient client;
    readonly DataQueue writeQueue = new DataQueue();
    readonly DataQueue readQueue = new DataQueue();
    NetworkStream networkStream;

    //通信が切断された時のイベント
    public event Action DisConnected;

    public MyTcpClient(string host, int port)
    {
        this.host = host;
        this.port = port;
    }

    //接続する
    public void Connect()
    {
        if (client != null) throw new Exception("clientは既に存在しています");
        client = new TcpClient(host, port);
        networkStream = client.GetStream();
        networkStream.WriteTimeout = 10000;
        new TcpReadWriteService().CreateReadWriteTask(networkStream, readQueue, writeQueue)
            .ContinueWith((_) => DisConnect());
    }

    //切断する
    public void DisConnect()
    {
        if (client == null) return;
        while (writeQueue.Length() != 0)
        {
            Thread.Sleep(100);
        }

        networkStream.Close();
        client.Close();
        client = null;
        DisConnected?.Invoke();
    }

    //データを書き込み

    public void WriteData(string str)
    {
        writeQueue.EnQueue(str + "\n");
    }

    //データの読み取り
    public DeQueueResult ReadData()
    {
        return readQueue.DeQueue();
    }
}