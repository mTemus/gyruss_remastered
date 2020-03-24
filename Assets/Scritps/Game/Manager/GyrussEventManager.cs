using System;
using UnityEngine;

public static class GyrussEventManager
{
    public static Action<StageType> StageTypeChangeInitiated;
    public static Action<Wave> WaveEnqueuingInitiated;
    public static Action<StageState> StageStateChangeInitiated;




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
    }
}
