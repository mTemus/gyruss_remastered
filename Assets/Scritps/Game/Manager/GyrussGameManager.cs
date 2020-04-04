using UnityEngine;

public class GyrussGameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GyrussEventManager gyrussEventManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private MinimapManager minimapManager;
    [SerializeField] private EffectsManager effectsManager;

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

    public void SetPlayerShipPosition(Vector3 playerShipPosition)
    {
        GyrussEventManager.OnPlayerShipPositionSetupInitiated(playerShipPosition);
    }

    public void RegisterReviveParticle(GameObject reviveParticle)
    {
        GyrussEventManager.OnReviveParticleRegistrationInitiated(reviveParticle);
    }

    public void PrepareReviveParticles()
    {
        GyrussEventManager.OnReviveParticlesPreparationInitiated();
    }

    public Vector3 GetPlayerShipPosition()
    {
        return GyrussEventManager.OnGetPlayerShipPositionInitiated();
    }

    public void SetPlayerEntered(bool entered)
    {
        GyrussEventManager.OnPlayerEnteredSetupInitiated(entered);
    }

    public void SetPlayerEnteredOnStage(bool condition)
    {
        GyrussEventManager.OnPlayerEnterOnStageInitiated(condition);
    }
    
    private void OnDestroy()
    {
        GyrussEventManager.ClearDelegates();
    }
    
    public static GyrussGameManager Instance => instance;
    
    public PlayerManager PlayerManager => playerManager;

    public StageManager StageManager => stageManager;
    
    public LevelManager LevelManager => levelManager;

    public TimeManager TimeManager => timeManager;

    public MinimapManager MinimapManager => minimapManager;

    public EffectsManager EffectsManager => effectsManager;
}
