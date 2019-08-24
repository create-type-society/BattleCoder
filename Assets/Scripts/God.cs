﻿using System;
using BattleCoder.GamePlayUi;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

//天地創造をする全てを支配する全知全能の神
public class God : MonoBehaviour
{
    [FormerlySerializedAs("botPrefab")] [SerializeField]
    BotEntity botEntityPrefab;

    [SerializeField] TileMapInfoManager tileMapInfoManagerPrefab;
    [SerializeField] CameraFollower cameraFollower;
    [SerializeField] PlayerHpPresenter playerHpPresenter;

    [SerializeField] RunButtonEvent runButtonEvent;
    [SerializeField] ScriptText scriptText;
    [SerializeField] BulletEntity bulletPrefab;
    [SerializeField] ErrorMsg errorMsg;

    BotEntityAnimation botEntityAnimation;

    BotApplication botApplication;

    JavaScriptEngine javaScriptEngine;
    IUserInput userInput = new KeyController();

    void Awake()
    {
        var tileMapInfo = Instantiate(tileMapInfoManagerPrefab).Create(SelectedStageData.GetSelectedStageKind());
        var botEntity = Instantiate(botEntityPrefab);
        cameraFollower.SetPlayerPosition(botEntity.transform);
        botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
        botApplication = new BotApplication(botEntity, botEntityAnimation, tileMapInfo, bulletPrefab);
        javaScriptEngine = new JavaScriptEngine(botApplication);
        runButtonEvent.AddClickEvent(() =>
        {
            try
            {
                javaScriptEngine.ExecuteJS(scriptText.GetScriptText());
            }
            catch (Exception e)
            {
                errorMsg.SetText(e.ToString());
            }
        });
        userInput.ShootingAttackEvent += (sender, e) => { botApplication.Shot(); };
    }

    void Update()
    {
        botApplication.Update();
        playerHpPresenter.RenderHp(botApplication.Hp);
        userInput.Update();

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (botApplication.Hp.IsDeath())
        {
            SceneChangeManager.ChangeResultScene(false);
        }
    }
}