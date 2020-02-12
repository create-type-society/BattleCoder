using BattleCoder.GamePlayUi;
using UnityEngine;

namespace BattleCoder.BotApplication.Bot.CommandObject
{
    public class GetKeyDownCommandObject : GetKeyCommandObject
    {
        public GetKeyDownCommandObject(EventSystemWatcher eventSystemWatcher, KeyCode keyCode) : base(eventSystemWatcher,
            keyCode)
        {
        }

        protected override bool GetKeyFunc()
        {
            return Input.GetKeyDown(keyCode);
        }
    }
}
