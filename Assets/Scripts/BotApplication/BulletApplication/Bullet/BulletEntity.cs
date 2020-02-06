using System;
using UnityEngine;

//弾の実体を表す
namespace BattleCoder.BotApplication.BulletApplication.Bullet
{
    public class BulletEntity : MonoBehaviour
    { 
        public event EventHandler<EventArgs> RockHitedEvent;
        private void OnTriggerEnter2D(Collider2D other)
        {
            RockHitedEvent.Invoke(this, EventArgs.Empty);
        }
    }
}