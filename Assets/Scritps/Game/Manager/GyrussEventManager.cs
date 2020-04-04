﻿using System;
using UnityEngine;

public class GyrussEventManager : MonoBehaviour
{
    public static Action<StageType> StageTypeSetupInitiated;
    public static Action<StageState> StageStateSetupInitiated;
    public static Action<int> CurrentWaveSetupInitiated;
    public static Action<Wave> WaveEnqueuingInitiated;
    public static Action<LevelState> LevelStateSetupInitiated;
    public static Action EnemyDeathInitiated;
    public static Func<int, Vector3> EnemySpotOccupationInitiated;
    public static Action<bool> WaveSpawnConditionSetInitiated;
    public static Action<bool> EnemySpawnConditionSetInitiated;
    public static Action PlayerArrivalOnMinimapInitiated;
    public static Action<int> MoveToLevelOnMinimapInitiated;
    
    public static void OnMoveToLevelOnMinimapInitiated(int levelIndex)
    {
        MoveToLevelOnMinimapInitiated?.Invoke(levelIndex);
    }
    
    public static void OnPlayerArrivalOnMinimapInitiated()
    {
        PlayerArrivalOnMinimapInitiated?.Invoke();
    }
    
    public static void OnCurrentWaveSetupInitiated(int currentWave)
    {
        CurrentWaveSetupInitiated?.Invoke(currentWave);
    }
    public static void OnWaveSpawnConditionSetInitiated(bool condition)
    {
        WaveSpawnConditionSetInitiated?.Invoke(condition);
    }

    public static void OnEnemySpawnConditionSetInitiated(bool condition)
    {
        EnemySpawnConditionSetInitiated?.Invoke(condition);
    }

    public static void OnLevelStateSetupInitiated(LevelState newLevelState)
    {
        LevelStateSetupInitiated?.Invoke(newLevelState);
    }
    
    public static Vector3 OnEnemySpotOccupationInitiated(int index)
    {
        if (EnemySpotOccupationInitiated != null) return (Vector3) 
            EnemySpotOccupationInitiated?.Invoke(index);
        
        return Vector3.zero;
    }
    
    public static void OnEnemyDeathInitiated()
    {
        EnemyDeathInitiated?.Invoke();
    }

    public static void OnStageTypeSetupInitiated(StageType newStageType)
    {
        StageTypeSetupInitiated?.Invoke(newStageType);
    }

    public static void OnWaveEnqueuingInitiated(Wave wave)
    {
        WaveEnqueuingInitiated?.Invoke(wave);
    }

    public static void OnStageStateSetupInitiated(StageState newStageState)
    {
        StageStateSetupInitiated?.Invoke(newStageState);
    }
    
    public static void ClearDelegates()
    {
        StageTypeSetupInitiated = null;
        WaveEnqueuingInitiated = null;
        StageStateSetupInitiated = null;
        EnemyDeathInitiated = null;
        EnemySpotOccupationInitiated = null;
        LevelStateSetupInitiated = null;
        WaveSpawnConditionSetInitiated = null;
        EnemySpawnConditionSetInitiated = null;
        CurrentWaveSetupInitiated = null;
        PlayerArrivalOnMinimapInitiated = null;
        MoveToLevelOnMinimapInitiated = null;
    }
}
