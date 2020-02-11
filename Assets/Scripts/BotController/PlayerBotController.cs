﻿using System.Threading;
using BattleCoder.BotApplication;
using BattleCoder.BotApplication.Bot;
using BattleCoder.BotApplication.BulletApplication.Bullet;
using BattleCoder.BotApplication.MeleeAttackApplication;
using BattleCoder.BotApplication.MeleeAttackApplication.MeleeAttack;
using BattleCoder.Common;
using BattleCoder.GamePlayUi;
using BattleCoder.Map;
using BattleCoder.Sound;
using UnityEngine;

namespace BattleCoder.BotController
{
    public class PlayerBotController : IBotController
    {
        readonly BotApplication.BotApplication botApplication;
        readonly PlayerHpPresenter playerHpPresenter;
        readonly JavaScriptEngine.JavaScriptEngine javaScriptEngine;
        readonly ErrorMsg errorMsg;

        public PlayerBotController(BotEntity botEntityPrefab, TileMapInfo tileMapInfo, BulletEntity bulletPrefab,
            CameraFollower cameraFollower, PlayerHpPresenter playerHpPresenter, RunButtonEvent runButtonEvent,
            ScriptText scriptText, ErrorMsg errorMsg, SoundManager soundManager, MeleeAttackEntity meleeAttackEntity,
            ProcessScrollViewPresenter processScrollViewPresenter, EventSystemWatcher eventSystemWatcher)
        {
            this.errorMsg = errorMsg;
            this.playerHpPresenter = playerHpPresenter;
            var botEntity = Object.Instantiate(botEntityPrefab);
            tileMapInfo.PlayerTankTransform = botEntity.transform;
            botEntity.gameObject.layer = LayerMask.NameToLayer("PlayerBot");
            cameraFollower.SetPlayerPosition(botEntity.transform);
            var botEntityAnimation = botEntity.GetComponent<BotEntityAnimation>();
            botEntity.transform.position = tileMapInfo.GetPlayer1StartPosition();
            MeleeAttackApplication meleeAttackApplication = new MeleeAttackApplication(meleeAttackEntity, soundManager);
            var gun = new Gun(
                soundManager,
                new BulletEntityCreator(bulletPrefab, LayerMask.NameToLayer("PlayerBullet")),
                false
            );
            botApplication = new BotApplication.BotApplication(
                botEntity, botEntityAnimation, tileMapInfo, eventSystemWatcher, gun, meleeAttackApplication
            );

            javaScriptEngine = new JavaScriptEngine.JavaScriptEngine(botApplication);
            runButtonEvent.AddClickEvent(() => OnRunButtonClick(processScrollViewPresenter, scriptText));
        }

        public void Update()
        {
            botApplication.Update();
            playerHpPresenter.RenderHp(botApplication.Hp);
            var errorText = javaScriptEngine.GetErrorText();
            if (errorText != "")
                errorMsg.SetText(errorText);
        }

        public Vector2 GetPos()
        {
            return botApplication.GetPos();
        }

        public void SetPos(Vector2 pos)
        {
            botApplication.SetPos(pos);
        }


        public bool IsDeath()
        {
            return botApplication.Hp.IsDeath();
        }

        public void Dispose()
        {
        }

        private async void OnRunButtonClick(ProcessScrollViewPresenter processScrollViewPresenter,
            ScriptText scriptText)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var panel = processScrollViewPresenter.AddProcessPanel(tokenSource.Cancel);
            var task = javaScriptEngine.ExecuteJS(scriptText.GetScriptText(), token, panel.ProcessId);

            await task;
            panel.Dispose();
        }
    }
}