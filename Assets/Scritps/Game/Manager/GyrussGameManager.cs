using System;
using UnityEngine;

public class GyrussGameManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private GyrussEventManager gyrussEventManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private StageManager stageManager;

    private static GyrussGameManager instance;

    private void Awake()
    {
        instance = this;
    }


    public void ChangeCurrentStageType(StageType newStageType)
    {
        GyrussEventManager.OnStageTypeChangeInitiated(newStageType);
    }

    public void EnqueueWave(Wave wave)
    {
        GyrussEventManager.OnWaveEnqueuingInitiated(wave);
    }

    public void ChangeStageState(StageState newStageState)
    {
        GyrussEventManager.OnStageStateChangeInitiated(newStageState);
    }
    


    private void OnDestroy()
    {
        GyrussEventManager.ClearDelegates();
    }

    public GyrussEventManager GyrussEventManager => gyrussEventManager;

    public static GyrussGameManager Instance => instance;
    
    public PlayerInputManager PlayerInputManager => playerInputManager;

    public StageManager StageManager => stageManager;
    
    public LevelManager LevelManager => levelManager;
}
