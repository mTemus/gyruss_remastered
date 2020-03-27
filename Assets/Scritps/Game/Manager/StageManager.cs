using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpots;

    private Dictionary<Transform, bool> enemySpotsMap;
    private Dictionary<Transform, GameObject> occupiedEnemySpots;

    private int currentStage = 1;
    private int enemiesAlive;

    private StageState currentStageState;
    private StageType currentStageType;
    private Wave currentWave;
    private Queue<Wave> wavesAwaiting;
    
    private void Start()
    {
        enemySpotsMap = new Dictionary<Transform, bool>();
        occupiedEnemySpots = new Dictionary<Transform, GameObject>();
        wavesAwaiting = new Queue<Wave>();

        currentStageState = StageState.start;
        SetDelegates();
        
        InitiateSpotsMap();
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
    }
    
    private void ProcessOrdersInStage()
    {
        switch (currentStageState) {
            case StageState.start:
                // timer needed
                // after displaying "ready", ship should appear
                // then enemies should spawn so state should change to loading_wave

                //TODO: check current stage type here
                if (currentStageType == StageType.normal) {
                    currentStageState = StageState.loading_wave;
                }

                break;
            
            case StageState.wait:
                // practically, should do nothing, for now, maybe wait for an event
                
                
                break;
            
            case StageState.loading_wave:
                if (wavesAwaiting.Count > 0) {
                    currentWave = wavesAwaiting.Dequeue();
                    GyrussGameManager.Instance.SetEnemySpawnCondition(true);
                }
                else {
                    Debug.LogError("Wave queue is empty!");
                    return;
                }
                currentStageState = StageState.spawn_enemies;
                
                break;
            
            case StageState.spawn_enemies:
                // spawning enemies
                
                Debug.Log("Enemy spawned");

                currentWave.EnemySpawned++;
                if (currentWave.EnemySpawned == currentWave.EnemyAmount) {
                    GyrussGameManager.Instance.SetEnemySpawnCondition(false);
                    currentStageState = StageState.wait;
                } 
                
                break;
            
            case StageState.end:
                ClearStage();

                currentStageState = StageState.wait;
                
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    
    private void InitiateSpotsMap()
    {
        foreach (Transform enemySpot in enemySpots) { enemySpotsMap[enemySpot] = false; }
    }

    private void AddEnemiesAlive(int enemiesAmount)
    {
        enemiesAlive += enemiesAmount;
    }

    private void ClearStage()
    {
        occupiedEnemySpots = new Dictionary<Transform, GameObject>();
        InitiateSpotsMap();
        
    }

    private void KillEnemy()
    {
        --enemiesAlive;
        
        if (enemiesAlive < 0) {
            Debug.LogError("You killed too much enemies!");
        }

        if (enemiesAlive == 0) {
            //TODO: next stage event will be added here
            GoToNextStage();
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
        if (currentStage > 4) { currentStage = 1; }
    }
    
    private void AddNewWave(Wave newWave)
    {
        wavesAwaiting.Enqueue(newWave);
    }

    private Vector3 OccupyEnemySpot(int index, GameObject enemy)
    {
        Transform t = enemySpots[index];

        if (enemySpotsMap[t]) return Vector3.zero;
        
        enemySpotsMap[t] = true;
        occupiedEnemySpots[enemySpots[index]] = enemy;
        return t.position;
    }

    public StageState CurrentStageState
    {
        get => currentStageState;
        set => currentStageState = value;
    }

    public StageType CurrentStageType
    {
        get => currentStageType;
        set => currentStageType = value;
    }

    public int CurrentStage => currentStage;

    public int EnemiesAlive => enemiesAlive;
}
