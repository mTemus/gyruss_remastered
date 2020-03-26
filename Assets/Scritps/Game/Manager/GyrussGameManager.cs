using UnityEngine;

public class GyrussGameManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private GyrussEventManager gyrussEventManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private TimeManager timeManager;

    private static GyrussGameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetCurrentStageType(StageType newStageType)
    {
        GyrussEventManager.OnStageTypeSetupInitiated(newStageType);
    }

    public void EnqueueWave(Wave wave)
    {
        GyrussEventManager.OnWaveEnqueuingInitiated(wave);
    }

    public void SetStageState(StageState newStageState)
    {
        GyrussEventManager.OnStageStateSetupInitiated(newStageState);
    }

    public void KillEnemy()
    {
        GyrussEventManager.OnEnemyDeathInitiated();
    }

    public Vector3 OccupyEnemySpot(int index, GameObject enemy)
    {
        return GyrussEventManager.OnEnemySpotOccupationInitiated(index, enemy);
    }

    public void SetLevelState(LevelState newLevelState)
    {
        GyrussEventManager.OnLevelStateSetupInitiated(newLevelState);
    }

    public StageState GetCurrentStageState()
    {
        return GyrussEventManager.OnCurrentStageStateGetInitiate();
    }

    public LevelState GetCurrentLevelState()
    {
        return GyrussEventManager.OnCurrentLevelStateGetInitiated();
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

    public TimeManager TimeManager => timeManager;
}
