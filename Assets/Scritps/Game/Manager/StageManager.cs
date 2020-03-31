﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpots;
    [SerializeField] private Transform mapCenterPoint;
    
    private int currentStage = 1;
    private int enemiesAlive;

    private int randomOfPath; // ???????????????????????????????

    private int evenSpotId = 0;
    private int unevenSpotId = 1;
    
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
                enemy.GetComponent<PathFollow>().mapCenter = mapCenterPoint;
                enemy.GetComponent<EnemyController>().SpotIndex = currentWave.IsWaveEven ? evenSpotId += 2 : unevenSpotId += 2;
                
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
    }

    private void ClearStage()
    {
        evenSpotId = 0;
        unevenSpotId = 1;
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

    private Vector3 OccupyEnemySpot(int index)
    {
        Transform t = enemySpots[index];
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
