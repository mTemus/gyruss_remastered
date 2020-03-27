using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private LevelState currentLevelState = LevelState.start;
    private StageState currentStageState = StageState.start;
    
    private float waveCreatingTimer = 30;
    private float waveSpawningTimer = 0;

    private bool createWave;
    private bool spawnEnemies;
    
    
    private void Start()
    {
        SetDelegates();
    }

    void Update()
    {
        if (createWave) {
            waveCreatingTimer -= Time.deltaTime;

            if (currentLevelState == LevelState.wait && waveCreatingTimer <= 0) {
                GyrussGameManager.Instance.SetLevelState(LevelState.create_wave);
                waveCreatingTimer = 30;
            }
        }
        
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



