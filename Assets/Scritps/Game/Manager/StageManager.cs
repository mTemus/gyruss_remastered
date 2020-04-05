using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [Header("Game views")] 
    [SerializeField] private GameObject MinimapView;
    [SerializeField] private GameObject StageView;
    
    [Header("StageView")] 
    [SerializeField] private Transform[] enemySpots;
    [SerializeField] private Transform mapCenterPoint;
    

    private int currentStage = 1;
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
                    randomOfPath = Random.Range(1, 8); // ???????????????
                    GyrussGameManager.Instance.SetEnemySpawnCondition(true);
                }
                else {
                    Debug.LogError("Wave queue is empty!");
                    return;
                }
                currentStageState = StageState.spawn_enemies;
                
                break;
            
            case StageState.spawn_enemies:
                GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/" + currentWave.EnemyName));
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
                
                IncreaseEnemyAlive();
                enemy.transform.name = currentWave.EnemyName + "_" + enemiesAlive;
                
                
                // Debug.Log("Enemy spawned");
                // Debug.Log(enemy.GetComponent<EnemyController>().SpotIndex);
                
                currentWave.EnemySpawned++;
                
                if (currentWave.EnemySpawned == currentWave.EnemyAmount) {
                    GyrussGameManager.Instance.SetEnemySpawnCondition(false);
                } 
                
                GyrussGameManager.Instance.SetStageState(StageState.wait);
                break;
            
            case StageState.end:
                ClearStage();

                currentStageState = StageState.wait;
                
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void IncreaseEnemyAlive()
    {
        enemiesAlive++;
        Debug.LogWarning(enemiesAlive + " created.");
    }

    private void ClearStage()
    {
        evenSpotId = 0;
        unevenSpotId = 1;
    }

    private void KillEnemy()
    {
        enemiesAlive--;
        Debug.LogError(enemiesAlive + " left.");
        
        if (enemiesAlive < 0) {
            Debug.LogError("You killed too much enemies!");
        }

        if (enemiesAlive == 0 && currentWaveCounter == 4) {
            //TODO: next stage event will be added here
            // GoToNextStage();
            Debug.LogError("next stage");
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

    private Vector3 OccupyEnemySpot(int index)
    {
        Transform t = enemySpots[index];
        return t.position;
    }

    private void SetCurrentWave(int currentWave)
    {
        currentWaveCounter = currentWave;
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
