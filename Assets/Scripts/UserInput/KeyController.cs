using System;
using UnityEngine;

// ユーザーからのキー入力
namespace BattleCoder.UserInput
{
    public class KeyController : IUserInput
    {
        // 近接攻撃のイベント
        public event EventHandler<EventArgs> MeleeAttackEvent;

        // 射撃攻撃のイベント
        public event EventHandler<EventArgs> ShootingAttackEvent;

        // 近接攻撃のキー
        private readonly KeyCode MeleeAttackKey = KeyCode.A;

        // 射撃攻撃のキー
        private readonly KeyCode ShootingAttackKey = KeyCode.S;

        // 入力更新メソッド
        public void Update()
        {
            // 近接攻撃のキーが押された時
            if (UnityEngine.Input.GetKeyDown(MeleeAttackKey))
            {
                OnMeleeAttackEvent(this, EventArgs.Empty);
            }

            // 射撃攻撃のキーが押された時
            if (UnityEngine.Input.GetKeyDown(ShootingAttackKey))
            {
                OnShootingAttack(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
        }

        private void OnMeleeAttackEvent(object sender, EventArgs args)
        {
            MeleeAttackEvent?.Invoke(sender, args);
        }

        private void OnShootingAttack(object sender, EventArgs args)
        {
            ShootingAttackEvent?.Invoke(sender, args);
        }
    }
}