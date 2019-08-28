using System.Collections;
using BattleCoder.Matching;
using UnityEngine;
using UnityEngine.UI;

public class MatchingUI : MonoBehaviour
{
    [SerializeField]private Text matchingText;
    MatchingServer matchingServer;
    readonly MyTcpClient client = new MyTcpClient("192.168.179.4", 3000);
    string text = "Matching Now...";
    int count = 0;
    IEnumerator title;
    IEnumerator selectStage;
    MatchType? type;
    private void Start()
    {
        title = WaitTitle();
        selectStage = WaitStageSelect();

        matchingText.text = text;
        client.Connect();
        matchingServer = new MatchingServer(client);
        matchingServer.Matched += (matchType) =>
        {
            type = matchType;
            MatchingInfo.Result = true;
        };
    }

    private void Update()
    {
        matchingServer.Update();
        count++;
        if (MatchingInfo.Result)
        {
            MatchingInfo.Result = false;
            matchingText.text = "マッチしました。";
            count = 0;
            if (type == MatchType.Host) StartCoroutine(selectStage);
            else
            { 
                matchingText.text = "Hostがステージを選択中です。";
            }
        }
        
        if (count == 600 )
        {
            if (type == null) matchingText.text = "マッチできませんでした。";
            else matchingText.text = "ステージが一定時間選択されなかったため切断します。";
            client.DisConnect();
            StartCoroutine(title);
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
    
    private IEnumerator WaitTitle()
    {
        yield return new WaitForSeconds(2.0f);
        SceneChangeManager.ChangeTitleScene();
    }
    
}
