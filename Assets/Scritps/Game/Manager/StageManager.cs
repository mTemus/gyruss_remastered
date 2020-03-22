using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private Transform[] enemySpots;

    private Dictionary<Transform, bool> enemySpotsMap;

    private int currentStage = 1;
    private int enemiesAlive;
    
    // Start is called before the first frame update
    private void Start()
    {
        enemySpotsMap = new Dictionary<Transform, bool>();
        
        InitiateSpotsMap();
    }

    // Update is called once per frame
    private void Update()
    {
        
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

    private void SetEnemySpotFree(int index)
    {
        Transform t = enemySpots[index];
        enemySpotsMap[t] = false;
    }

    private Vector3 GetEnemySpotPosition(int index)
    {
        Transform t = enemySpots[index];

        if (enemySpotsMap[t]) return Vector3.negativeInfinity;
        
        SetEnemySpotOccupied(index);
        return t.position;
    }

    private void AddEnemiesAlive(int enemiesAmount)
    {
        enemiesAlive += enemiesAmount;
    }


    public int CurrentStage => currentStage;

    public int EnemiesAlive => enemiesAlive;
}
