﻿using BattleCoder.BotApplication.Bot;
using BattleCoder.BotApplication.BulletApplication.Bullet;
using BattleCoder.BotApplication.MeleeAttackApplication.MeleeAttack;
using BattleCoder.Common;
using BattleCoder.GamePlayUi;
using BattleCoder.Map;
using BattleCoder.Sound;

namespace BattleCoder.PlayGame
{
    public struct PlayGameInitData
    {
        public readonly BotEntity botEntityPrefab;
        public readonly BotEntity botEntityPrefab2P;
        public readonly CameraFollower cameraFollower;
        public readonly PlayerHpPresenter playerHpPresenter;
        public readonly TileMapInfo tileMapInfo;
        public readonly RunButtonEvent runButtonEvent;
        public readonly ScriptText scriptText;
        public readonly BulletEntity bulletPrefab;
        public readonly ErrorMsg errorMsg;
        public readonly SoundManager soundManager;
        public readonly MeleeAttackEntity meleeAttackPrefab;
        public readonly ProcessScrollViewPresenter processScrollViewPresenter;
        public readonly EventSystemWatcher eventSystemWatcher;

        public PlayGameInitData(
            BotEntity botEntityPrefab,
            BotEntity botEntityPrefab2P,
            CameraFollower cameraFollower,
            PlayerHpPresenter playerHpPresenter,
            TileMapInfo tileMapInfo,
            RunButtonEvent runButtonEvent,
            ScriptText scriptText,
            BulletEntity bulletPrefab,
            ErrorMsg errorMsg,
            SoundManager soundManager,
            MeleeAttackEntity meleeAttackPrefab,
            ProcessScrollViewPresenter processScrollViewPresenter,
            EventSystemWatcher eventSystemWatcher)
        {
            this.botEntityPrefab = botEntityPrefab;
            this.botEntityPrefab2P = botEntityPrefab2P;
            this.cameraFollower = cameraFollower;
            this.playerHpPresenter = playerHpPresenter;
            this.tileMapInfo = tileMapInfo;
            this.runButtonEvent = runButtonEvent;
            this.scriptText = scriptText;
            this.bulletPrefab = bulletPrefab;
            this.errorMsg = errorMsg;
            this.soundManager = soundManager;
            this.meleeAttackPrefab = meleeAttackPrefab;
            this.processScrollViewPresenter = processScrollViewPresenter;
            this.eventSystemWatcher = eventSystemWatcher;
        }
    }
}