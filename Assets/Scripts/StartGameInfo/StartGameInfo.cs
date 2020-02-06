using System;

namespace BattleCoder.StartGameInfo
{
    public static class StartGameInfo
    {
        public static bool IsSinglePlay { get; private set; } = true;

        static MultiGameInfo? multiGameInfo = null;

        public static void SetSinglePlay()
        {
            IsSinglePlay = true;
        }

        public static void SetMultiPlay(MultiGameInfo multiGameInfo)
        {
            IsSinglePlay = false;
            StartGameInfo.multiGameInfo = multiGameInfo;
        }


        public static MultiGameInfo GetMultiGameInfo()
        {
            if (multiGameInfo.HasValue == false) throw new Exception("MultiGameInfoの取得に失敗");
            if (IsSinglePlay) throw new Exception("SinglePlayです");
            var v = multiGameInfo.Value;
            multiGameInfo = null;
            return v;
        }
    }
}