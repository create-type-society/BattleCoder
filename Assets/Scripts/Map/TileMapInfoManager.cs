//TileMapInfoを管理する

using System;
using UnityEngine;

public class TileMapInfoManager : MonoBehaviour
{
    [SerializeField] TileMapInfo testStage;
    [SerializeField] TileMapInfo stage1;
    [SerializeField] TileMapInfo stage2;
    [SerializeField] TileMapInfo stage3;

    public TileMapInfo Create(StageKind stageKind)
    {
        switch (stageKind)
        {
            case StageKind.TestStage:
                return Instantiate(testStage);
            case StageKind.Stage1:
                return Instantiate(stage1);
                break;
            case StageKind.Stage2:
                return Instantiate(stage2);
            case StageKind.Stage3:
                return Instantiate(stage3);
            default:
                throw new ArgumentOutOfRangeException(nameof(stageKind), stageKind, null);
        }

        throw new NotImplementedException();
    }
}