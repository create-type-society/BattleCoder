//マルチプレイするために必要な事前情報

using BattleCoder.Matching;
using BattleCoder.Tcp;

namespace BattleCoder.StartGameInfo
{
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
}