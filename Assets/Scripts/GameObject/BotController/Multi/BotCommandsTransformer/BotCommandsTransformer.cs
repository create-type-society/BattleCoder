using System;

namespace DefaultNamespace
{
    public class BotCommandsTransformerService
    {
        public void FromCommandData(CommandData commandData, IBotCommands botCommands)
        {
            switch (commandData.kind)
            {
                case CommandKind.Move:
                    botCommands.Move((Direction) commandData.parameters[0], (uint) commandData.parameters[1]);
                    break;
                case CommandKind.MoveDirection:
                    botCommands.MoveDirection((Direction) commandData.parameters[0]);
                    break;
                case CommandKind.MoveShotRotation:
                    botCommands.MoveShotRotation((float) commandData.parameters[0]);
                    break;
                case CommandKind.Shot:
                    botCommands.Shot();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}