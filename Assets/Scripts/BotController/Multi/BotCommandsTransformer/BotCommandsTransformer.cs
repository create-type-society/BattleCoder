using System;
using BattleCoder.BotApplication;
using BattleCoder.Common;
using BattleCoder.GameSignaling;

namespace BattleCoder.BotController.Multi.BotCommandsTransformer
{
    public class BotCommandsTransformerService
    {
        public void FromCommandData(CommandData commandData, IBotCommands botCommands)
        {
            switch (commandData.kind)
            {
                case CommandKind.Move:
                    botCommands.Move((Direction) (Int64) commandData.parameters[0],
                        (uint) (Int64) commandData.parameters[1]);
                    break;
                case CommandKind.MoveDirection:
                    botCommands.MoveDirection((Direction) (Int64) commandData.parameters[0]);
                    break;
                case CommandKind.MoveShotRotation:
                    botCommands.MoveShotRotation((float) (double) commandData.parameters[0]);
                    break;
                case CommandKind.Shot:
                    botCommands.Shot();
                    break;
                case CommandKind.MeleeAttack:
                    botCommands.MeleeAttack();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}