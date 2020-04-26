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
    [SerializeField] private Transform enemyPool;

    private int currentStage = 1;
    private int stages = 1;
    private int enemiesAlive;

    private int randomOfPath; // ???????????????????????????????

    private int evenSpotId = 0;
    private int unevenSpotId = 1;
    private int currentWaveCounter;
    private int modulesAmount = 4;
    private int moduleId;
    
    private StageState currentStageState;
    private StageType currentStageType;
    private Wave currentWave;

    private List<GameObject> miniBossModules;
    private Dictionary<GameObject, List<GameObject>> modulesAwaitingForShips;
    private Queue<Wave> wavesAwaiting;
    
    private void Start()
    {
        modulesAwaitingForShips = new Dictionary<GameObject, List<GameObject>>();
        miniBossModules = new List<GameObject>();
        wavesAwaiting = new Queue<Wave>();
        
        SetDelegates();
        GyrussGameManager.Instance.SetStageState(StageState.wait);
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
        GyrussEventManager.StageClearInitiated += ClearStage;
        GyrussEventManager.MiniBossModuleKillInitiated += DestroyMiniBossModule;
        GyrussEventManager.ShipRemovalFromAwaitingListInitiated += RemoveShipFromModule;
        GyrussEventManager.AllEnemiesDeleteInitiated += DeleteAllEnemies;
    }
    
    private void ProcessOrdersInStage()
    {
        switch (currentStageState) {
            case StageState.start:
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
                        // prepare bonuses spawning here
                        
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                GyrussGameManager.Instance.SetStageState(StageState.wait);
                break;
            
            case StageState.wait:
                break;
            
            case StageState.loading_wave:
                if (wavesAwaiting.Count > 0) {
                    currentWave = wavesAwaiting.Dequeue();
                    
                    if (!currentWave.MiniBoss) {
                        GyrussGameManager.Instance.SetConditionInTimer("enemySpawningDelay", true);
                    }
                }
                else {
                    Debug.LogError("Wave queue is empty!");
                    return;
                }
                
                GyrussGameManager.Instance.SetStageState(StageState.spawn_enemies);
                break;
            
            case StageState.spawn_enemies:
                if (currentWave.MiniBoss) {
                    SpawnMiniBoss();
                }
                else {
                    SpawnEnemy();
                    IncreaseEnemyAlive();
                }
                
                GyrussGameManager.Instance.SetStageState(StageState.wait);
                break;

            case StageState.get_ready:
                if (playerShip.activeSelf) {
                    if (currentStageType == StageType.first_stage) {
                        GyrussGameManager.Instance.ToggleScoreText();
                    }
                    
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
                PlayStageBGM();
                GyrussGameManager.Instance.PrepareReviveParticles();
                GyrussGameManager.Instance.SetStagesText(stages);
                GyrussGameManager.Instance.SetStageState(StageState.initialize_GUI);
                break;
            
            case StageState.end:
                if (!GyrussGameManager.Instance.MovePlayerShipToWarpingPosition()) return;

                if (GyrussGameManager.Instance.MovePlayerShipToCenterPosition()) {
                    GyrussGameManager.Instance.ClearCurrentStage();
                    playerShip.SetActive(false);
                    
                    GyrussGameManager.Instance.TogglePlayerSpawned();
                    GyrussGameManager.Instance.SetCurrentWave(currentStageType == StageType.first_stage ? 0 : 1);

                    if (currentStageType == StageType.boss) GyrussGameManager.Instance.DestroyPlanet();
                    
                    GyrussGameManager.Instance.SetLevelState(currentStageType == StageType.chance
                        ? LevelState.change_view_to_minimap
                        : LevelState.start);

                    GyrussGameManager.Instance.SetStageState(StageState.wait);
                }
                else {
                    GyrussGameManager.Instance.WarpPlayer();
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void IncreaseEnemyAlive()
    {
        enemiesAlive++;
    }

    private void PlayStageBGM()
    {
        switch (currentStageType) {
            case StageType.first_stage:
                if (!GyrussGameManager.Instance.IsBGMPlaying()) 
                    GyrussGameManager.Instance.PlayBGM("stage-1-bgm");
                break;
                        
            case StageType.mini_boss:
                GyrussGameManager.Instance.PlayBGM("stage-2-bgm");
                break;
                        
            case StageType.boss:
                GyrussGameManager.Instance.PlayBGM("stage-3-bgm");
                break;
                        
            case StageType.chance:
                GyrussGameManager.Instance.PlayBGM("stage-4-bgm");
                break;
        }
    }
    
    private void ClearStage()
    {
        evenSpotId = 0;
        unevenSpotId = 1;
    }

    private void KillEnemy()
    {
        if (playerShip == null) return; 
        
        enemiesAlive--;

        if (enemiesAlive < 0) {
            Debug.LogError("You killed too much enemies!");
        }
        
        switch (enemiesAlive) {
            case 0 when currentWaveCounter == 4:
                if (currentStageType == StageType.boss) {
                    GyrussGameManager.Instance.StopCurrentPlayingBGM();
                    GyrussGameManager.Instance.PlayBGM("stage-3-boss");
                    GyrussGameManager.Instance.SpawnBoss(); 
                }
                    
                else if(currentStageType != StageType.chance) {
                    GyrussGameManager.Instance.SilenceCurrentPlayingBGM();
                    GyrussGameManager.Instance.SetConditionInTimer("nextStageDelay", true);
                }
                break;
            
            case 0 when currentWaveCounter == 5:
                GyrussGameManager.Instance.SilenceCurrentPlayingBGM();
                GyrussGameManager.Instance.StartCountingChanceBonusPoints();
                break;
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

        if (currentStage > 4) { currentStage = 1; }
        
        GyrussGameManager.Instance.StopTimer("asteroidSpawn");
        GyrussGameManager.Instance.SetStageState(StageState.end);
        GyrussGameManager.Instance.SetCurrentStageNumber(currentStage);
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
        switch (currentWave) {
            case 5 when currentStageType != StageType.chance:
                return;
            
            case 5 when currentStageType == StageType.chance:
                currentWaveCounter = currentWave;
                break;
            
            case 0:
                currentWaveCounter = 1;
                break;
            
            default:
                currentWaveCounter = currentWave;
                break;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/" + currentWave.EnemyName), enemyPool, true);
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

        currentEnemyController.WaveId = currentWaveCounter;
        currentEnemyController.CurrentEnemyState = EnemyStates.take_a_spot;
        currentEnemyController.CurrentStageType = currentStageType;
        
        enemy.transform.name = currentWave.EnemyName + "_" + enemiesAlive;

        switch (currentStageType) {
            case StageType.mini_boss:
                if (modulesAmount == 0) Destroy(enemy); 
                
                modulesAwaitingForShips[miniBossModules[moduleId]].Add(enemy);
                enemy.transform.SetParent(miniBossModules[moduleId].transform);
                enemy.GetComponent<EnemyController>().MyModule = miniBossModules[moduleId];
                enemy.GetComponent<PositionsUpadator>().SetModuleToUpdate(miniBossModules[moduleId].transform);
                moduleId++;
                
                if (moduleId > modulesAmount - 1) moduleId = 0;
                break;
            
            case StageType.chance:
                enemy.GetComponent<PositionsUpadator>().enabled = false;
                break;
        }

        currentWave.EnemySpawned++;
                
        if (currentWave.EnemySpawned != currentWave.EnemyAmount) {
            GyrussGameManager.Instance.SetConditionInTimer("enemySpawningDelay", true);
        } 
    }
    
    private void SpawnMiniBoss()
    {
        int angle = 0;
        
        for (int i = 0; i < 4; i++) {
            GameObject enemyModule = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/" + currentWave.EnemyName), enemyPool, true);
            enemyModule.transform.position = new Vector3(0, 0.4f, 0);
            enemyModule.transform.RotateAround(Vector3.zero, Vector3.forward, angle);
            enemyModule.transform.name = "Mini_boss_module_" + i;

            miniBossModules.Add(enemyModule);
            modulesAwaitingForShips[enemyModule] = new List<GameObject>();
            IncreaseEnemyAlive();
            angle += 90;
        }

        currentWave = null;
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
                GyrussGameManager.Instance.SetPeriodInTimer("asteroidSpawn", 5);
                GyrussGameManager.Instance.SetConditionInTimer("asteroidSpawn", true);
                break;
            
            case StageType.boss:
                GyrussGameManager.Instance.ResetPeriodInTimer("asteroidSpawn");
                GyrussGameManager.Instance.SetPeriodInTimer("asteroidSpawn", 9);
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
            GameObject asteroid = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Asteroid"), enemyPool, true);
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

    private void DestroyMiniBossModule(GameObject module)
    {
        miniBossModules.Remove(module);
        modulesAmount--;
        moduleId = 0;

        foreach (GameObject ship in modulesAwaitingForShips[module]) {
            GameObject newModule = SetNewMiniBossModule();

            if (newModule == null) {
                GyrussGameManager.Instance.KillEnemy();
                GyrussGameManager.Instance.AddPointsToScore(100);
                GyrussGameManager.Instance.CreateExplosion(ship.transform.position, "normal");
                Destroy(ship);
            }
            else {
                ship.GetComponent<EnemyController>().MyModule = newModule;
                ship.GetComponent<PositionsUpadator>().SetModuleToUpdate(newModule.transform);
            }
        }
    }

    private GameObject SetNewMiniBossModule()
    {
        if (modulesAmount == 0) { return null; }
        
        GameObject module = miniBossModules[moduleId++];
        moduleId++;

        if (moduleId > modulesAmount - 1) {
            moduleId = 0;
        }

        return module;
    }

    private void RemoveShipFromModule(GameObject module, GameObject ship)
    {
        if (!modulesAwaitingForShips[module].Contains(ship)) return; 
        modulesAwaitingForShips[module].Remove(ship);
    }

    private void DeleteAllEnemies()
    {
        foreach (Transform enemy in enemyPool) {
            Destroy(enemy.gameObject);
        }
    }
    
    public int CurrentStage => currentStage; 
}
