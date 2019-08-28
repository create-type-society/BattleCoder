using System;
using UnityEngine;

namespace BattleCoder.Matching
{
    public class MatchingCancel:MonoBehaviour
    {
        public event EventHandler<EventArgs> MatchingCancelEvent;

        public void OnClick()
        {
            OnMatchingCancelEvent(this,EventArgs.Empty);
        }
        
        public void OnMatchingCancelEvent(object sender, EventArgs args)
        {
            MatchingCancelEvent?.Invoke(sender, args);
        }
    }
}