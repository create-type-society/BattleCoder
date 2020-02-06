using BattleCoder.Map;
using UnityEngine;

//選択したステージの種別を保存する
namespace BattleCoder.StageTransition
{
    public static class SelectedStageData
    {
        static StageKind? selectedStageKind = null;

        //選択ステージ種別をセットする
        public static void SetSelectedStageKind(StageKind stageKind)
        {
            selectedStageKind = stageKind;
        }

        //選択ステージ種別を取得する
        public static StageKind GetSelectedStageKind()
        {
            if (selectedStageKind.HasValue)
                return selectedStageKind.Value;
            Debug.Log("Stageが設定されませんでしたので、TestStageを設定します");
            return StageKind.TestStage;
        }
    }
}