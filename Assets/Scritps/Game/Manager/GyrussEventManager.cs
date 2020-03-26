using System;
using UnityEngine;

public class GyrussEventManager : MonoBehaviour
{
    public static Action<StageType> StageTypeSetupInitiated;
    public static Action<StageState> StageStateSetupInitiated;
    public static Action<Wave> WaveEnqueuingInitiated;
    public static Action<LevelState> LevelStateSetupInitiated;
    public static Action EnemyDeathInitiated;
    public static Func<int, GameObject, Vector3> EnemySpotOccupationInitiated;
    public static Func<StageState> CurrentStageStateGetInitiated;
    public static Func<LevelState> CurrentLevelStateGetInitiated;

    public StageState OnCurrentStageStateGetInitiate()
    {
        if (CurrentStageStateGetInitiated != null) 
            return (StageState) CurrentStageStateGetInitiated?.Invoke();

        return StageState.no_state;
    }

    public LevelState OnCurrentLevelStateGetInitiated()
    {
        if (CurrentLevelStateGetInitiated != null) 
            return (LevelState) CurrentLevelStateGetInitiated?.Invoke();

        return LevelState.no_state;
    }
    
    public static void OnLevelStateSetupInitiated(LevelState newLevelState)
    {
        LevelStateSetupInitiated?.Invoke(newLevelState);
    }
    
    public static Vector3 OnEnemySpotOccupationInitiated(int index, GameObject enemy)
    {
        if (EnemySpotOccupationInitiated != null) return (Vector3) 
            EnemySpotOccupationInitiated?.Invoke(index, enemy);
        
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
        CurrentLevelStateGetInitiated = null;
        CurrentStageStateGetInitiated = null;
    }
}
