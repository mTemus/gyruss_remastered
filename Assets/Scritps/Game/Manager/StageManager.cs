using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpots;

    private Dictionary<Transform, bool> enemySpotsMap;
    private Dictionary<Transform, GameObject> occupiedEnemySpots;

    private int currentStage = 1;
    private int enemiesAlive;

    private StageState currentState;
    private Wave currentWave;
    private Queue<Wave> wavesAwaiting;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        enemySpotsMap = new Dictionary<Transform, bool>();
        occupiedEnemySpots = new Dictionary<Transform, GameObject>();
        wavesAwaiting = new Queue<Wave>();

        currentState = StageState.start;
        
        InitiateSpotsMap();
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentState == StageState.wait) return; 
        ProcessOrdersInStage();
    }

    private void ProcessOrdersInStage()
    {
        switch (currentState) {
            case StageState.start:
                // timer needed
                // after displaying "ready", ship should appear
                // then enemies should spawn so state should change to loading_wave



                currentState = StageState.loading_wave;
                break;
            case StageState.wait:
                // practically, should do nothing, for now, maybe wait for an event
                
                
                break;
            case StageState.loading_wave:
                if (wavesAwaiting.Count > 0) { currentWave = wavesAwaiting.Dequeue(); }
                else {
                    Debug.LogError("Wave queue is empty!");
                    return;
                }
                currentState = StageState.spawn_enemies;
                
                break;
            case StageState.spawn_enemies:
                // spawning enemies


                if (currentWave.EnemySpawned == currentWave.EnemyAmount) { currentState = StageState.wait; }
                
                break;
            case StageState.end:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    
    private void InitiateSpotsMap()
    {
        foreach (Transform enemySpot in enemySpots) { enemySpotsMap[enemySpot] = false; }
    }

    private void SetEnemySpotOccupied(int index)
    {
        Transform t = enemySpots[index];
        enemySpotsMap[t] = true;
    }

    private void OccupyEnemySpot(int index, GameObject enemy)
    {
        occupiedEnemySpots[enemySpots[index]] = enemy;
    }

    private void AddEnemiesAlive(int enemiesAmount)
    {
        enemiesAlive += enemiesAmount;
    }

    
    public void SetEnemySpotFree(int index)
    {
        Transform t = enemySpots[index];
        enemySpotsMap[t] = false;
    }

    public void LeaveEnemySpot(GameObject enemy)
    {
        occupiedEnemySpots.Remove(occupiedEnemySpots.FirstOrDefault(key => key.Value == enemy).Key);
    }
    
    public Vector3 GetEnemySpotPosition(int index, GameObject enemy)
    {
        Transform t = enemySpots[index];

        if (enemySpotsMap[t]) return Vector3.negativeInfinity;
        
        SetEnemySpotOccupied(index);
        OccupyEnemySpot(index, enemy);
        return t.position;
    }

    public void AddNewWave(Wave newWave)
    {
        wavesAwaiting.Enqueue(newWave);
    }
    
    public int CurrentStage => currentStage;

    public int EnemiesAlive => enemiesAlive;
}
