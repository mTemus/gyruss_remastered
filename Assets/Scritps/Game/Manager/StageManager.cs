using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("StageView")] 
    [SerializeField] private Transform[] enemySpots;
    [SerializeField] private Transform mapCenterPoint;

    [Header("PlayerShip")] 
    [SerializeField] private GameObject playerShip;

    [Header("Pools")] 
    [SerializeField] private Transform EnemyPool;
    

    private int currentStage = 1;
    private int stages = 1;
    private int enemiesAlive;

    private int randomOfPath; // ???????????????????????????????

    private int evenSpotId = 0;
    private int unevenSpotId = 1;
    private int currentWaveCounter;
    
    private StageState currentStageState;
    private StageType currentStageType;
    private Wave currentWave;
    private Queue<Wave> wavesAwaiting;
    
    private void Start()
    {
        wavesAwaiting = new Queue<Wave>();
        currentStageState = StageState.start;
        currentStageType = StageType.first_stage;
        SetDelegates();
    }

    private void Update()
    {
        if (currentStageState == StageState.wait) return; 
        ProcessOrdersInStage();
    }

    private void SetDelegates()
    {
        GyrussEventManager.StageTypeSetupInitiated += SetNewStageType;
        GyrussEventManager.StageStateSetupInitiated += SetNewStageState;
        GyrussEventManager.WaveEnqueuingInitiated += AddNewWave;
        GyrussEventManager.EnemyDeathInitiated += KillEnemy;
        GyrussEventManager.EnemySpotOccupationInitiated += OccupyEnemySpot;
        GyrussEventManager.CurrentWaveSetupInitiated += SetCurrentWave;
        GyrussEventManager.GoToNextStageInitiated += GoToNextStage;
        GyrussEventManager.AsteroidSpawnInitiated += SpawnAsteroid;
    }
    
    private void ProcessOrdersInStage()
    {
        switch (currentStageState) {
            case StageState.start:
                // timer needed
                // after displaying "ready", ship should appear
                // then enemies should spawn so state should change to loading_wave
                
                switch (currentStageType) {
                    case StageType.no_type:
                        break;
                    
                    case StageType.first_stage:
                        PrepareAsteroidSpawning();
                        break;
                    
                    case StageType.mini_boss:
                        PrepareAsteroidSpawning();
                        break;
                    
                    case StageType.boss:
                        PrepareAsteroidSpawning();
                        break;
                    
                    case StageType.chance:
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                GyrussGameManager.Instance.SetStageState(StageState.wait);
                break;
            
            case StageState.wait:
                // practically, should do nothing, for now, maybe wait for an event
                break;
            
            case StageState.loading_wave:
                if (wavesAwaiting.Count > 0) {
                    currentWave = wavesAwaiting.Dequeue();
                    // randomOfPath = Random.Range(1, 8); // ???????????????
                    
                    GyrussGameManager.Instance.SetConditionInTimer("enemySpawningDelay", true);
                }
                else {
                    Debug.LogError("Wave queue is empty!");
                    return;
                }
                
                GyrussGameManager.Instance.SetStageState(StageState.spawn_enemies);
                break;
            
            case StageState.spawn_enemies:
                SpawnEnemy();
                IncreaseEnemyAlive();
                
                GyrussGameManager.Instance.SetStageState(StageState.wait);
                break;
            
            case StageState.end:
                if (!GyrussGameManager.Instance.MovePlayerShipToWarpingPosition()) return;

                if (GyrussGameManager.Instance.MovePlayerShipToCenterPosition()) {
                    
                    ClearStage();
                    //Proceed to next stage
                    GyrussGameManager.Instance.SetStageState(StageState.wait);
                }
                else {
                    GyrussGameManager.Instance.WarpPlayer();
                }
                
                break;

            case StageState.get_ready:
                if (playerShip.activeSelf) {
                    GyrussGameManager.Instance.ToggleScoreText();
                    GyrussGameManager.Instance.SetLevelState(LevelState.create_wave);
                }
                break;
            
            case StageState.initialize_GUI:
                if (currentStageType == StageType.first_stage) {
                    GyrussGameManager.Instance.InitializeLifeIcons();
                    GyrussGameManager.Instance.InitializeRocketIcons();
                }
                
                GyrussGameManager.Instance.SetStageState(StageState.get_ready);
                break;
            
            case StageState.spawn_player:
                GyrussGameManager.Instance.PrepareReviveParticles();
                GyrussGameManager.Instance.SetStageState(StageState.initialize_GUI);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void IncreaseEnemyAlive()
    {
        enemiesAlive++;
    }

    private void ClearStage()
    {
        evenSpotId = 0;
        unevenSpotId = 1;
    }

    private void KillEnemy()
    {
        enemiesAlive--;

        if (enemiesAlive < 0) {
            Debug.LogError("You killed too much enemies!");
        }

        if (currentStageType == StageType.first_stage || currentStageType == StageType.chance) {
            if (enemiesAlive == 0 && currentWaveCounter == 4) {
                GyrussGameManager.Instance.SetConditionInTimer("nextStageDelay", true);
            } else if (enemiesAlive == 0 && currentWaveCounter == 5) {
                
            }
        }
    }
    
    private void SetNewStageType(StageType newStageType)
    {
        currentStageType = newStageType;
    }

    private void SetNewStageState(StageState newStageState)
    {
        currentStageState = newStageState;
    }
    
    private void GoToNextStage()
    {
        currentStage++;
        stages++;
        
        GyrussGUIEventManager.OnStagesTextSetupInitiated(stages);

        if (currentStage > 4) { currentStage = 1; }
        
        GyrussGameManager.Instance.TogglePlayerSpawned();
        GyrussGameManager.Instance.SetStageState(StageState.end);
    }
    
    private void AddNewWave(Wave newWave)
    {
        wavesAwaiting.Enqueue(newWave);
    }

    private Transform OccupyEnemySpot(int index)
    {
        Transform t = enemySpots[index];
        return t;
    }

    private void SetCurrentWave(int currentWave)
    {
        if (currentWave == 5 && currentStageType != StageType.chance) 
            return;
        
        currentWaveCounter = currentWave;
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/" + currentWave.EnemyName), EnemyPool, true);
        enemy.transform.position = new Vector3(-100, -100, 0);
        enemy.GetComponent<PathFollow>().mapCenter = mapCenterPoint;
        EnemyController currentEnemyController = enemy.GetComponent<EnemyController>();
                
        if (currentWave.IsWaveEven) {
            currentEnemyController.SpotIndex = evenSpotId;
            evenSpotId += 2;
        }
        else {
            currentEnemyController.SpotIndex = unevenSpotId;
            unevenSpotId += 2;
        }
                
        currentEnemyController.currentEnemyState = EnemyStates.take_a_spot;
        
        enemy.transform.name = currentWave.EnemyName + "_" + enemiesAlive;
                
        currentWave.EnemySpawned++;
                
        if (currentWave.EnemySpawned != currentWave.EnemyAmount) {
            GyrussGameManager.Instance.SetConditionInTimer("enemySpawningDelay", true);
        } 
    }

    private void PrepareAsteroidSpawning()
    {
        switch (currentStageType) {
            case StageType.no_type:
                break;
            
            case StageType.first_stage:
                GyrussGameManager.Instance.SetConditionInTimer("asteroidSpawn", true);
                break;
            
            case StageType.mini_boss:
                GyrussGameManager.Instance.SetPeriodInTimer("asteroidSpawn", 3);
                GyrussGameManager.Instance.SetConditionInTimer("asteroidSpawn", true);
                break;
            
            case StageType.boss:
                GyrussGameManager.Instance.ResetPeriodInTimer("asteroidSpawn");
                GyrussGameManager.Instance.SetPeriodInTimer("asteroidSpawn", 7);
                GyrussGameManager.Instance.SetConditionInTimer("asteroidSpawn", true);
                break;
            
            case StageType.chance:
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        } 
    }
    
    private void SpawnAsteroid()
    {
        if (currentStageState == StageState.wait || currentStageState == StageState.spawn_enemies) {
            GameObject asteroid = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Asteroid"), EnemyPool, true);
            asteroid.transform.position = Vector3.zero;

            AsteroidController myAsteroidController = asteroid.GetComponent<AsteroidController>();
            myAsteroidController.PlayerPosition = playerShip.transform.position;
            myAsteroidController.CalculateExitPosition();

            GyrussGameManager.Instance.SetConditionInTimer("asteroidSpawn", true);
        }
        else {
            GyrussGameManager.Instance.SetConditionInTimer("asteroidSpawn", true);
        }
    }

    public int CurrentStage => currentStage; 
}
