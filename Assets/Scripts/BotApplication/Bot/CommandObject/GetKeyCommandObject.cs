using BattleCoder.GamePlayUi;
using UnityEngine;

namespace BattleCoder.BotApplication.Bot.CommandObject
{
    public class GetKeyCommandObject : BaseCommandObject<bool>
    {
        readonly EventSystemWatcher eventSystemWatcher;
        protected readonly KeyCode keyCode;

        public GetKeyCommandObject(EventSystemWatcher eventSystemWatcher, KeyCode keyCode)
        {
            this.eventSystemWatcher = eventSystemWatcher;
            this.keyCode = keyCode;
        }

        public override void Run()
        {
            result = GetKeyFunc() && !eventSystemWatcher.IsInputFieldFocused();
            Finished();
        }

        protected virtual bool GetKeyFunc()
        {
            return Input.GetKey(keyCode);
        }
    }
}