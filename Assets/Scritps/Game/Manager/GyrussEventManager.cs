﻿using System;
using UnityEngine;

public class GyrussEventManager : MonoBehaviour
{
    public static Func<Vector3> GetPlayerShipPositionInitiated;
    public static Func<int, Vector3> EnemySpotOccupationInitiated;
    public static Func<int> PlayerLivesGetInitiated;
    public static Func<int> PlayerRocketsGetInitiated;
    public static Action EnemyDeathInitiated;
    public static Action ReviveParticlesPreparationInitiated;
    public static Action PlayerShipSpawnInitiated;
    public static Action PlayerSpawnedToggleInitiated;
    public static Action<StageType> StageTypeSetupInitiated;
    public static Action<StageState> StageStateSetupInitiated;
    public static Action<LevelState> LevelStateSetupInitiated;
    public static Action<Wave> WaveEnqueuingInitiated;
    public static Action<Vector3> PlayerShipPositionSetupInitiated;
    public static Action<Vector3> ExplosionCreationInitiated;
    public static Action<GameObject> ReviveParticleRegistrationInitiated;
    public static Action<int> MoveToLevelOnMinimapInitiated;
    public static Action<int> CurrentWaveSetupInitiated;
    public static Action<bool> PlayerEnteredSetupInAnimatorInitiated;
    public static Action<string, bool> ConditionSetupInTimerInitiated;

    public static void OnPlayerSpawnedToggleInitiated()
    {
        PlayerSpawnedToggleInitiated?.Invoke();
    }
    
    public static void OnExplosionCreationInitiated(Vector3 explosionPosition)
    {
        ExplosionCreationInitiated?.Invoke(explosionPosition);
    }
    
    public static int OnPlayerRocketsGetInitiated() => (int) PlayerRocketsGetInitiated?.Invoke();
    
    public static int OnPlayerLivesGetInitiated() => (int) PlayerLivesGetInitiated?.Invoke();

    public static void OnConditionSetupInTimerInitiated(string timerName, bool timerCondition)
    {
        ConditionSetupInTimerInitiated?.Invoke(timerName, timerCondition);
    }

    public static void OnPlayerShipSpawnInitiated()
    {
        PlayerShipSpawnInitiated?.Invoke();
    }
    
    public static void OnPlayerEnteredSetupInAnimatorInitiated(bool entered)
    {
        PlayerEnteredSetupInAnimatorInitiated?.Invoke(entered);
    }
    
    public static Vector3 OnGetPlayerShipPositionInitiated() => (Vector3) GetPlayerShipPositionInitiated?.Invoke();
    
    public static void OnReviveParticlesPreparationInitiated()
    {
        ReviveParticlesPreparationInitiated?.Invoke();
    }
    
    public static void OnReviveParticleRegistrationInitiated(GameObject reviveParticle)
    {
        ReviveParticleRegistrationInitiated?.Invoke(reviveParticle);
    }
    
    public static void OnPlayerShipPositionSetupInitiated(Vector3 playerShipPosition)
    {
        PlayerShipPositionSetupInitiated?.Invoke(playerShipPosition);
    }
    
    public static void OnMoveToLevelOnMinimapInitiated(int levelIndex)
    {
        MoveToLevelOnMinimapInitiated?.Invoke(levelIndex);
    }
    
    public static void OnCurrentWaveSetupInitiated(int currentWave)
    {
        CurrentWaveSetupInitiated?.Invoke(currentWave);
    }

    public static void OnLevelStateSetupInitiated(LevelState newLevelState)
    {
        LevelStateSetupInitiated?.Invoke(newLevelState);
    }
    
    public static Vector3 OnEnemySpotOccupationInitiated(int index) => (Vector3) EnemySpotOccupationInitiated?.Invoke(index);
    
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
        CurrentWaveSetupInitiated = null;
        MoveToLevelOnMinimapInitiated = null;
        PlayerShipPositionSetupInitiated = null;
        ReviveParticleRegistrationInitiated = null;
        ReviveParticlesPreparationInitiated = null;
        GetPlayerShipPositionInitiated = null;
        PlayerEnteredSetupInAnimatorInitiated = null;
        PlayerShipSpawnInitiated = null;
        ConditionSetupInTimerInitiated = null;
        PlayerLivesGetInitiated = null;
        PlayerRocketsGetInitiated = null;
        ExplosionCreationInitiated = null;
        PlayerSpawnedToggleInitiated = null;
    }
}
