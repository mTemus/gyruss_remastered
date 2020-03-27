using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private LevelState currentLevelState = LevelState.start;
    private StageState currentStageState = StageState.start;

    private float waveCreatingTimer;
    private float enemySpawningTimer;

    private float waveCreatingPeriod = 6f;
    private float enemySpawnPeriod = 0.5f;
    
    private bool createWave;
    private bool spawnEnemies;
    
    
    private void Start()
    {
        waveCreatingTimer = waveCreatingPeriod;
        enemySpawningTimer = enemySpawnPeriod;
        SetDelegates();
    }

    void Update()
    {
        CheckWaveCreation();
        CheckEnemySpawning();

        
    }

    private void CheckWaveCreation()
    {
        if (!createWave) return;
        waveCreatingTimer -= Time.deltaTime;

        if (currentLevelState != LevelState.wait || !(waveCreatingTimer <= 0)) return;
        
        GyrussGameManager.Instance.SetLevelState(LevelState.create_wave);
        waveCreatingTimer = waveCreatingPeriod;
    }

    private void CheckEnemySpawning()
    {
        if (!spawnEnemies) return;
        enemySpawningTimer -= Time.deltaTime;

        if (currentStageState != StageState.wait || !(enemySpawningTimer <= 0)) return;
        
        GyrussGameManager.Instance.SetStageState(StageState.spawn_enemies);
        enemySpawningTimer = enemySpawnPeriod;
    }
    
    

    private void SetDelegates()
    {
        GyrussEventManager.StageStateSetupInitiated += GetCurrentStageState;
        GyrussEventManager.LevelStateSetupInitiated += GetCurrentLevelState;
        GyrussEventManager.EnemySpawnConditionSetInitiated += SetEnemySpawnCondition;
        GyrussEventManager.WaveSpawnConditionSetInitiated += SetSpawnWaveCondition;
    }

    private void GetCurrentStageState(StageState newStageState)
    {
        currentStageState = newStageState;
    }

    private void GetCurrentLevelState(LevelState newLevelState)
    {
        currentLevelState = newLevelState;
    }

    private void SetSpawnWaveCondition(bool condition)
    {
        createWave = condition;
    }

    private void SetEnemySpawnCondition(bool condition)
    {
        spawnEnemies = condition;
    }
}



