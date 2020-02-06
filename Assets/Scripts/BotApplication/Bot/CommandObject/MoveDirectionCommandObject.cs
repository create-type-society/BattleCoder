// 向きの移動

using System;
using BattleCoder.Common;
using Void = BattleCoder.Common.Void;

namespace BattleCoder.BotApplication.Bot.CommandObject
{
    public class MoveDirectionCommandObject : BaseCommandObject<Void>
    {
        readonly BotEntity botEntity;
        readonly BotEntityAnimation botEntityAnimation;
        readonly Direction direction;
        readonly Action directionChangeCallback;

        public MoveDirectionCommandObject(BotEntity botEntity, BotEntityAnimation botEntityAnimation, Direction direction,
            Action directionChangeCallback)
        {
            this.botEntity = botEntity;
            this.botEntityAnimation = botEntityAnimation;
            this.direction = direction;
            this.directionChangeCallback = directionChangeCallback;
        }

        public override void Run()
        {
            directionChangeCallback();
            botEntityAnimation.MoveAnimation(direction, false);
            Finished();
        }
    }
}