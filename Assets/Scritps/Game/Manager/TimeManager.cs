using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private LevelState currentLevelState = LevelState.start;
    private StageState currentStageState = StageState.start;

    private float waveCreatingTimer;
    private float enemySpawningTimer;
    private float minimapArriveTimer;
    private float playerEnteredStageTimer;
    private float playerStayedOnMinimapTimer;

    private float waveCreatingPeriod = 6f;
    private float enemySpawnPeriod = 0.3f;
    private float playerArrivalPeriod = 2f;
    private float playerEnteredPeriod = 1f;
    private float playerStayedOnMinimapPeriod = 1f;
    
    private bool createWave;
    private bool spawnEnemies;
    private bool playerArrived = true;
    private bool playerEntered;
    private bool playerStayed;
    
    
    private void Start()
    {
        waveCreatingTimer = waveCreatingPeriod;
        enemySpawningTimer = enemySpawnPeriod;
        playerEnteredStageTimer = playerEnteredPeriod;
        playerStayedOnMinimapTimer = playerStayedOnMinimapPeriod;
        SetDelegates();
    }

    private void Update()
    {
        CheckPlayerStayOnMinimap();
        CheckPlayerArrivalOnMinimap();
        CheckWaveCreation();
        CheckEnemySpawning();
        CheckPlayerEnterStage();
    }
    
    private void SetDelegates()
    {
        GyrussEventManager.StageStateSetupInitiated += GetCurrentStageState;
        GyrussEventManager.LevelStateSetupInitiated += GetCurrentLevelState;
        GyrussEventManager.EnemySpawnConditionSetInitiated += SetEnemySpawnCondition;
        GyrussEventManager.WaveSpawnConditionSetInitiated += SetSpawnWaveCondition;
        GyrussEventManager.PlayerArrivalOnMinimapInitiated += TogglePlayerArrival;
        GyrussEventManager.PlayerEnteredOnStageConditionSetInitiated += SetPlayerEnterCondition;
        GyrussEventManager.PlayerStayedOnMinimapConditionInitiated += setPlayerStayedCondition;
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


    private void CheckPlayerArrivalOnMinimap()
    {
        if (playerArrived) return;
        minimapArriveTimer -= Time.deltaTime;

        if (minimapArriveTimer <= 0) {
            playerArrived = true;
            GyrussGameManager.Instance.SetLevelState(LevelState.initialize_GUI);
            minimapArriveTimer = playerArrivalPeriod;
        }
    }

    private void CheckPlayerEnterStage()
    {
        if(!playerEntered) return;
        playerEnteredStageTimer -= Time.deltaTime;

        if (playerEnteredStageTimer <= 0) {
            playerEntered = false;
            GyrussGameManager.Instance.SetPlayerEnteredInAnimator(false);
            playerEnteredStageTimer = playerEnteredPeriod;
        }
    }

    private void CheckPlayerStayOnMinimap()
    {
        if(!playerStayed) return;
        playerStayedOnMinimapTimer -= Time.deltaTime;

        if (playerStayedOnMinimapTimer <= 0) {
            playerStayed = false;
            GyrussGameManager.Instance.SetLevelState(LevelState.move_on_minimap);
            playerStayedOnMinimapTimer = playerStayedOnMinimapPeriod;
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

    private void setPlayerStayedCondition(bool condition)
    {
        playerStayed = condition;
    }
}



