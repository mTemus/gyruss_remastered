using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private LevelState currentLevelState = LevelState.start;
    private StageState currentStageState = StageState.start;

    private float waveCreatingTimer;
    private float enemySpawningTimer;
    private float minimapArriveTimer;
    private float playerEnteredTimer;

    private float waveCreatingPeriod = 6f;
    private float enemySpawnPeriod = 0.3f;
    private float playerArrivalPeriod = 0.6f;
    private float playerEnteredPeriod = 1f;
    
    private bool createWave;
    private bool spawnEnemies;
    private bool playerArrived = true;
    private bool playerEntered;
    
    
    private void Start()
    {
        waveCreatingTimer = waveCreatingPeriod;
        enemySpawningTimer = enemySpawnPeriod;
        playerEnteredTimer = playerEnteredPeriod;
        SetDelegates();
    }

    private void Update()
    {
        CheckWaveCreation();
        CheckEnemySpawning();
        CheckPlayerArrival();
        CheckPlayerEnter();
    }
    
    private void SetDelegates()
    {
        GyrussEventManager.StageStateSetupInitiated += GetCurrentStageState;
        GyrussEventManager.LevelStateSetupInitiated += GetCurrentLevelState;
        GyrussEventManager.EnemySpawnConditionSetInitiated += SetEnemySpawnCondition;
        GyrussEventManager.WaveSpawnConditionSetInitiated += SetSpawnWaveCondition;
        GyrussEventManager.PlayerArrivalOnMinimapInitiated += TogglePlayerArrival;
        GyrussEventManager.PlayerEnteredOnStageConditionSetInitiated += SetPlayerEnterCondition;
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


    private void CheckPlayerArrival()
    {
        if (playerArrived) return;
        minimapArriveTimer -= Time.deltaTime;

        if (minimapArriveTimer <= 0) {
            //TODO: event arrival in LevelManager (change state or something)

            playerArrived = true;
            minimapArriveTimer = playerArrivalPeriod;
        }
    }

    private void CheckPlayerEnter()
    {
        if(!playerEntered) return;
        playerEnteredTimer -= Time.deltaTime;
        Debug.Log(playerEnteredTimer);

        if (playerEnteredTimer <= 0) {
            playerEntered = false;
            GyrussGameManager.Instance.SetPlayerEnteredInAnimator(false);
            playerEnteredTimer = playerEnteredPeriod;
        }
    }

    private void TogglePlayerArrival()
    {
        playerArrived = !playerArrived;
        minimapArriveTimer = playerArrivalPeriod;
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

    private void SetPlayerEnterCondition(bool condition)
    {
        playerEntered = condition;
    }
}



