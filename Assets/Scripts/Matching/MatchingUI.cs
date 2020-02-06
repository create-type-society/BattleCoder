using System;
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
            Application.quitting += client.DisConnect;

            matchingText.text = text;
            try
            {
                client.Connect();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                SceneChangeManager.ChangeTitleScene();
                return;
            }

            matchingClient = new MatchingClient(client, async data =>
            {
                if (data.MatchingDataType == MatchingDataType.MatchedData)
                {
                    matchingText.text = "マッチしました。";
                    if (data.MatchType == MatchType.Host) await WaitStageSelect(data.MatchType);
                    else
                    {
                        matchingText.text = "Hostがステージを選択中です。";
                    }
                }
                else
                {
                    if (data.MatchType != MatchType.Client) return;
                    await WaitGamePlay(data.StageKind, data.MatchType);
                    matchingText.text = "ステージが決定されました。ゲームを開始します。";
                }
            });

            cancel.MatchingCancelEvent += (sender, args) =>
            {
                client.DisConnect();
                SceneChangeManager.ChangeTitleScene();

                matchingClient.MatchingDataObserver.OnError(new OperationCanceledException());
            };
        }

        void Update()
        {
            matchingClient?.Update();
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