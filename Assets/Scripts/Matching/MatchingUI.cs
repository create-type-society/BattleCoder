using System.Collections;
using BattleCoder.Matching;
using UnityEngine;
using UnityEngine.UI;

public class MatchingUI : MonoBehaviour
{
    [SerializeField]private Text matchingText;
    MatchingClient matchingClient;
    readonly MyTcpClient client = new MyTcpClient("192.168.179.4", 3000);
    [SerializeField]MatchingCancel cancel;
    string text = "Matching Now...";
    IEnumerator selectStage;
    MatchType? type;
    
    private void Start()
    {
        cancel.MatchingCancelEvent += (sender, args) =>
        {
            client.DisConnect();
            SceneChangeManager.ChangeTitleScene();
        };
        
        selectStage = WaitStageSelect();

        matchingText.text = text;
        client.Connect();
        matchingClient = new MatchingClient(client);
        matchingClient.Matched += (matchType) =>
        {
            type = matchType;
            MatchingInfo.Result = true;
        };
    }

    private void Update()
    {
        matchingClient.Update();
        if (MatchingInfo.Result)
        {
            MatchingInfo.Result = false;
            matchingText.text = "マッチしました。";
            if (type == MatchType.Host) StartCoroutine(selectStage);
            else
            { 
                matchingText.text = "Hostがステージを選択中です。";
            }
        }

        if (MatchingInfo.StageSlected)
        {
            matchingText.text = "ステージが決定されました。ゲームを開始します。";
            StartCoroutine(selectStage);
        }
    }

    private IEnumerator WaitStageSelect()
    {
        yield return new WaitForSeconds (2.0f);  
        SceneChangeManager.ChangeStageSelect();
    }
}
