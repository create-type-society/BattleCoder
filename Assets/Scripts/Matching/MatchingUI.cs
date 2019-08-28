using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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
    private void Start()
    {
        title = WaitTitle();
        selectStage = WaitStageSelect();

        matchingText.text = text;
        client.Connect();
        matchingServer = new MatchingServer(client);
        matchingServer.Matched += (matchType) =>
        {
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
            StartCoroutine(selectStage);
        }
        if (count == 600)
        {
            matchingText.text = "マッチできませんでした。";
            client.DisConnect();
            StartCoroutine(title);
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
