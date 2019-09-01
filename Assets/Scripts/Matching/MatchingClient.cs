using System;


public class MatchingClient
{
    MyTcpClient myTcpClient;

    //マッチ成功した時に呼ばれる時に呼ばれる処理
    public event Action<MatchType> Matched;

    //ステージ決定した時に呼ばれる処理
    public event Action<StageKind> StageDetermined;

    public MatchingClient(MyTcpClient myTcpClient)
    {
        this.myTcpClient = myTcpClient;
    }

    public void Update()
    {
        while (true)
        {
            var result = myTcpClient.ReadData();
            if (result.isOk == false) return;
            if (result.data == "match_host")
            {
                Matched?.Invoke(MatchType.Host);
                return;
            }
            else if (result.data == "match_client")
            {
                Matched?.Invoke(MatchType.Client);
                return;
            }
            else if (result.data.IndexOf("stage_kind:") == 0)
            {
                StageDetermined?.Invoke(
                    (StageKind) int.Parse(result.data.Split(':')[1])
                );
            }
        }
    }
}