//マルチプレイするために必要な事前情報

public struct MultiGameInfo
{
    readonly public MyTcpClient myTcpClient;
    readonly public MatchType matchType;

    public MultiGameInfo(MyTcpClient myTcpClient, MatchType matchType)
    {
        this.myTcpClient = myTcpClient;
        this.matchType = matchType;
    }
}