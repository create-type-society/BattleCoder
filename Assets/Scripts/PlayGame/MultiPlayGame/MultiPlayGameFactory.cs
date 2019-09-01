using System;
using Object = UnityEngine.Object;

public static class MultiPlayGameFactory
{
    public static IPlayGame CreateMultiPlayGame(PlayGameInitData playGameInitData)
    {
        var multiGameInfo = StartGameInfo.GetMultiGameInfo();
        if (multiGameInfo.matchType == MatchType.Host)
            return new HostMultiPlayGame(playGameInitData, multiGameInfo);
        return new ClientMultiPlayGame(playGameInitData, multiGameInfo);
    }
}