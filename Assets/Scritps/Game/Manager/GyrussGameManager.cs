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

    public void SetWaveSpawnCondition(bool condition)
    {
        GyrussEventManager.OnWaveSpawnConditionSetInitiated(condition);
    }

    public void SetEnemySpawnCondition(bool condition)
    {
        GyrussEventManager.OnEnemySpawnConditionSetInitiated(condition);
    }

    public Vector3 OccupyEnemySpot(int index)
    {
        return GyrussEventManager.OnEnemySpotOccupationInitiated(index);
    }

    public void SetLevelState(LevelState newLevelState)
    {
        GyrussEventManager.OnLevelStateSetupInitiated(newLevelState);
    }

    public void SetCurrentWave(int currentWave)
    {
        GyrussEventManager.OnCurrentWaveSetupInitiated(currentWave);
    }
    
    public void TogglePlayerArrivalOnMinimap()
    {
        GyrussEventManager.OnPlayerArrivalOnMinimapInitiated();
    }

    public void MoveToLevelOnMinimap(int levelIndex)
    {
        GyrussEventManager.OnMoveToLevelOnMinimapInitiated(levelIndex);
    }

    private void OnDestroy()
    {
        GyrussEventManager.ClearDelegates();
    }
    
    public static GyrussGameManager Instance => instance;
    
    public PlayerInputManager PlayerInputManager => playerInputManager;

    public StageManager StageManager => stageManager;
    
    public LevelManager LevelManager => levelManager;

    public TimeManager TimeManager => timeManager;
}
