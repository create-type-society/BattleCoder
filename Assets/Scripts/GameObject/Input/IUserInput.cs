﻿using System;

// ユーザー入力インタフェース
public interface IUserInput
{
    // 近接攻撃のイベント
    event EventHandler<EventArgs> MeleeAttackEvent;

    // 射撃攻撃のイベント
    event EventHandler<EventArgs> ShootingAttackEvent;

    // 入力更新メソッド
    void Update();
}