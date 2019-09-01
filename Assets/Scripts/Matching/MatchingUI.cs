using System.Collections;
using BattleCoder.Matching;
using UnityEngine;
using UnityEngine.UI;

public class MatchingUI : MonoBehaviour
{
    [SerializeField] private Text matchingText;
    MatchingClient matchingClient;
    readonly MyTcpClient client = new MyTcpClient("localhost", 3000);
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
        matchingClient.Matched += (matchType) =>
        {
            type = matchType;
            matchingText.text = "マッチしました。";
            if (type == MatchType.Host) StartCoroutine(WaitStageSelect(type.Value));
            else
            {
                matchingText.text = "Hostがステージを選択中です。";
            }
        };
        matchingClient.StageDetermined += stageKind =>
        {
            if (type != MatchType.Client) return;
            StartCoroutine(WaitGamePlay(stageKind, type.Value));
            matchingText.text = "ステージが決定されました。ゲームを開始します。";
        };
    }

    void Update()
    {
        matchingClient.Update();

        //           StartCoroutine(selectStage);
    }

    IEnumerator WaitStageSelect(MatchType matchType)
    {
        yield return new WaitForSeconds(2.0f);
        SceneChangeManager.ChangeMultiPlayStageSelect(new MultiGameInfo(client, matchType));
    }

    IEnumerator WaitGamePlay(StageKind stageKind, MatchType matchType)
    {
        yield return new WaitForSeconds(0.5f);
        SceneChangeManager.ChangeClientMultiPlayScene(stageKind, new MultiGameInfo(client, matchType));
    }
}