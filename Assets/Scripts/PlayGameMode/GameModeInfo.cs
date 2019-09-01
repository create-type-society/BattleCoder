using System;

public static class GameModeInfo
{
    static GameModeKind? gameModeKind = null;

    public static void SetGameMode(GameModeKind gameModeKind)
    {
        GameModeInfo.gameModeKind = gameModeKind;
    }

    public static GameModeKind GetGameMode()
    {
        if (gameModeKind.HasValue) return gameModeKind.Value;
        throw new Exception("GameModeKindが設定されていません");
    }
}