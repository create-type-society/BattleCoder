using BattleCoder.GamePlayUi;
using UnityEngine;

namespace BattleCoder.BotApplication.Bot.CommandObject
{
    public class GetKeyUpCommandObject : GetKeyCommandObject
    {
        public GetKeyUpCommandObject(EventSystemWatcher eventSystemWatcher, KeyCode keyCode) : base(eventSystemWatcher,
            keyCode)
        {
        }

        protected override bool GetKeyFunc()
        {
            return Input.GetKeyUp(keyCode);
        }
    }
}