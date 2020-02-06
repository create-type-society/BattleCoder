using System;
using BattleCoder.Map;
using BattleCoder.Tcp;
using UniRx;

namespace BattleCoder.Matching
{
    public class MatchingClient
    {
        MyTcpClient myTcpClient;

        public IObserver<MatchingData> MatchingDataObserver { get; }

        public MatchingClient(MyTcpClient myTcpClient, Action<MatchingData> callback)
        {
            this.myTcpClient = myTcpClient;
            MatchingDataObserver = Observer.Create(callback);
        }

        public void Update()
        {
            while (true)
            {
                var result = myTcpClient.ReadData();
                if (result.isOk == false) return;
                if (result.data == "match_host")
                {
                    MatchingDataObserver.OnNext(new MatchingData(MatchType.Host));
                    return;
                }
                else if (result.data == "match_client")
                {
                    MatchingDataObserver.OnNext(new MatchingData(MatchType.Client));
                    return;
                }
                else if (result.data.IndexOf("stage_kind:") == 0)
                {
                    MatchingDataObserver.OnNext(new MatchingData(
                        (StageKind) int.Parse(result.data.Split(':')[1])
                    ));
                    MatchingDataObserver.OnCompleted();
                }
            }
        }
    }
}