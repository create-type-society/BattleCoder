using System;
using Void = BattleCoder.Common.Void;

namespace BattleCoder.BotApplication.Bot.CommandObject
{
    public class MoveShotRotationCommandObject : BaseCommandObject<Void>
    {
        readonly Action changeRotationCallback;

        public MoveShotRotationCommandObject(Action changeRotationCallback)
        {
            this.changeRotationCallback = changeRotationCallback;
        }

        public override void Run()
        {
            changeRotationCallback();
            Finished();
        }
    }
}