using BattleCoder.Map;

namespace BattleCoder.Matching
{
    public struct MatchingData
    {
        private MatchType? _matchType;
        private StageKind? _stageKind;

        public MatchingDataType MatchingDataType { get; }

        public MatchType? MatchType => _matchType;
        public StageKind? StageKind => _stageKind;

        public MatchingData(MatchType matchType)
        {
            MatchingDataType = MatchingDataType.MatchedData;
            _stageKind = null;
            _matchType = matchType;
        }

        public MatchingData(StageKind stageKind)
        {
            MatchingDataType = MatchingDataType.StageDeterminedData;
            _stageKind = stageKind;
            _matchType = null;
        }
    }
}