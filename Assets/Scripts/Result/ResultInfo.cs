using System;

namespace BattleCoder.Result
{
    public static class ResultInfo
    {
        private static bool? result = null;

        public static void SetResult(bool result)
        {
            ResultInfo.result = result;
        }
        public static bool GetResult()
        {
            if (result.HasValue) return result.Value;
            throw new NullReferenceException("resultがnullなんだよ！！");
        }    
    
    }
}
