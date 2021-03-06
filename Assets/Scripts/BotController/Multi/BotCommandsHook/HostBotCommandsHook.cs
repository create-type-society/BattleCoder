﻿using System;
using System.Threading.Tasks;
using BattleCoder.BotApplication;
using BattleCoder.Common;
using BattleCoder.GameSignaling;
using BattleCoder.Map;
using BattleCoder.Matching;
using UnityEngine;
using Void = BattleCoder.Common.Void;

namespace BattleCoder.BotController.Multi.BotCommandsHook
{
    public class HostBotCommandsHook : IBotCommands
    {
        readonly IBotCommands botCommands;
        readonly GameSignalingHost gameSignalingHost;

        public HostBotCommandsHook(IBotCommands botCommands, GameSignalingHost gameSignalingHost)
        {
            this.botCommands = botCommands;
            this.gameSignalingHost = gameSignalingHost;
        }

        public Task<Void> Move(Direction direction, uint gridDistance)
        {
            SendCommandData(
                new CommandData(0, CommandKind.Move, 0, new object[] {direction, gridDistance})
            );
            return botCommands.Move(direction, gridDistance);
        }

        public void Coroutine(uint frameTime, Action action)
        {
            botCommands.Coroutine(frameTime, action);
        }

        public Task<Void> MoveDirection(Direction direction)
        {
            SendCommandData(
                new CommandData(0, CommandKind.MoveDirection, 0, new object[] {direction})
            );
            return botCommands.MoveDirection(direction);
        }

        public Task<Void> MoveShotRotation(float rotation)
        {
            SendCommandData(
                new CommandData(0, CommandKind.MoveShotRotation, 0, new object[] {rotation})
            );
            return botCommands.MoveShotRotation(rotation);
        }

        public Task<GridPosition> GetMyPosition()
        {
            return botCommands.GetMyPosition();
        }

        public Task<float> GetPositionAngle(GridPosition position)
        {
            return botCommands.GetPositionAngle(position);
        }

        public Task<TileType> GetTileType(GridPosition position)
        {
            return botCommands.GetTileType(position);
        }

        public Task<bool> GetKey(KeyCode keyCode)
        {
            return botCommands.GetKey(keyCode);
        }

        public Task<bool> GetKeyUp(KeyCode keyCode)
        {
            return botCommands.GetKeyUp(keyCode);
        }

        public Task<bool> GetKeyDown(KeyCode keyCode)
        {
            return botCommands.GetKeyDown(keyCode);
        }

        public async Task<bool> Shot()
        {
            if (await botCommands.Shot() == false) return false;
            SendCommandData(new CommandData(0, CommandKind.Shot, 0, new object[] { }));
            return true;
        }

        public async Task<Void> MeleeAttack()
        {
            SendCommandData(new CommandData(0, CommandKind.MeleeAttack, 0, new object[] { }));
            return await botCommands.MeleeAttack();
        }

        public Task<bool> BoolUnityFunc(Func<bool> f)
        {
            return botCommands.BoolUnityFunc(f);
        }

        void SendCommandData(CommandData commandData)
        {
            gameSignalingHost.SendData(new ClientReceiveSignalData(
                commandData,
                MatchType.Host
            ));
        }
    }
}