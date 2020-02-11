using System;

// 入力された攻撃命令を受け取るインタフェース
namespace BattleCoder.AttackInput
{
    public interface IAttackInput : IDisposable
    {
        // 近接攻撃のイベント
        event EventHandler<EventArgs> MeleeAttackEvent;

        // 射撃攻撃のイベント
        event EventHandler<EventArgs> ShootingAttackEvent;

        // 入力更新メソッド
        void Update();
    }
}