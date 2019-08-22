//TileMapInfoを管理する

using System;
using UnityEngine;

public class TileMapInfoManager : MonoBehaviour
{
    [SerializeField] TileMapInfo testStage;

    public TileMapInfo Create(StageKind stageKind)
    {
        switch (stageKind)
        {
            case StageKind.TestStage:
                return Instantiate(testStage);
            case StageKind.Stage1:
                break;
            case StageKind.Stage2:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stageKind), stageKind, null);
        }

        throw new NotImplementedException();
    }
}