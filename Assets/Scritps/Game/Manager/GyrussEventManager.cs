using System;
using UnityEngine;

public class GyrussEventManager : MonoBehaviour
{
    public static Action<StageType> StageTypeChangeInitiated;
    public static Action<Wave> WaveEnqueuingInitiated;
    public static Action<StageState> StageStateChangeInitiated;
    public static Action EnemyDeathInitiated;
    public static Func<int, GameObject, Vector3> EnemySpotOccupationInitiated;

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

    public static void OnStageTypeChangeInitiated(StageType newStageType)
    {
        StageTypeChangeInitiated?.Invoke(newStageType);
    }

    public static void OnWaveEnqueuingInitiated(Wave wave)
    {
        WaveEnqueuingInitiated?.Invoke(wave);
    }

    public static void OnStageStateChangeInitiated(StageState newStageState)
    {
        StageStateChangeInitiated?.Invoke(newStageState);
    }


    public static void ClearDelegates()
    {
        StageTypeChangeInitiated = null;
        WaveEnqueuingInitiated = null;
        StageStateChangeInitiated = null;
        EnemyDeathInitiated = null;
        EnemySpotOccupationInitiated = null;
    }
}
