using BattleCoder.Map;
using BattleCoder.StartGameInfo;
using BattleCoder.Tcp;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCoder.Matching
{
    public class MatchingUI : MonoBehaviour
    {
        [SerializeField] private Text matchingText;
        MatchingClient matchingClient;
        readonly MyTcpClient client = new MyTcpClient("133.167.115.186", 3000);
        [SerializeField] MatchingCancel cancel;
        string text = "Matching Now...";

        void Start()
        {
            cancel.MatchingCancelEvent += (sender, args) =>
            {
                client.DisConnect();
                SceneChangeManager.ChangeTitleScene();
            };

            Application.quitting += client.DisConnect;

            matchingText.text = text;
            client.Connect();

            MatchType? type = null;
            matchingClient = new MatchingClient(client);
            matchingClient.Matched += async matchType =>
            {
                type = matchType;
                matchingText.text = "マッチしました。";
                if (type == MatchType.Host) await WaitStageSelect(type.Value);
                else
                {
                    matchingText.text = "Hostがステージを選択中です。";
                }
            };
            matchingClient.StageDetermined += async stageKind =>
            {
                if (type != MatchType.Client) return;
                await WaitGamePlay(stageKind, type.Value);
                matchingText.text = "ステージが決定されました。ゲームを開始します。";
            };
        }

        void Update()
        {
            matchingClient.Update();
        }

        async UniTask WaitStageSelect(MatchType matchType)
        {
            await UniTask.Delay(2000);
            SceneChangeManager.ChangeMultiPlayStageSelect(new MultiGameInfo(client, matchType));
        }

        async UniTask WaitGamePlay(StageKind stageKind, MatchType matchType)
        {
            await UniTask.Delay(500);
            SceneChangeManager.ChangeClientMultiPlayScene(stageKind, new MultiGameInfo(client, matchType));
        }
    }
}