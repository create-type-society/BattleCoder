using System;

// ユーザー入力インタフェース
namespace BattleCoder.Input
{
    public interface IUserInput : IDisposable
    {
        // 近接攻撃のイベント
        event EventHandler<EventArgs> MeleeAttackEvent;

        // 射撃攻撃のイベント
        event EventHandler<EventArgs> ShootingAttackEvent;

        // 入力更新メソッド
        void Update();
    }
}