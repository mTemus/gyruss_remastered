using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel = 0;
    private int currentWave = 1;

    private String enemyName;

    private float waveLoadingTimer = 0;
    private float waveLoadTime = 30f;

    private LevelState currentLevelState = LevelState.start;

    private void Start()
    {
        SetDelegates();
    }

    private void Update()
    {
         // if current state == wait => return
         
         ProcessOrdersInLevel();

    }

    private void SetDelegates()
    {
        GyrussEventManager.LevelStateSetupInitiated += SetLevelState;
    }

    private void ProcessOrdersInLevel()
    {
        switch (currentLevelState) {
            case LevelState.start:
                // mini-map operations
                // initializing player ship on every level

                GyrussGameManager.Instance.MoveToLevelOnMinimap(currentLevel);
                
                currentLevelState = LevelState.wait;
                return;
                
                currentLevelState = LevelState.create_wave;
                
                break;
            case LevelState.wait:
                
                break;
            case LevelState.create_wave:
                enemyName = "Enemy_L" + currentLevel + "_S" + GyrussGameManager.Instance.StageManager.CurrentStage + "_T";
                GyrussGameManager.Instance.SetCurrentWave(currentWave);
                
                    switch (currentWave) {
                        case 1:
                            SetCurrentStageType();
                            SetWaveToSpawn(1, false);
                            GyrussGameManager.Instance.SetWaveSpawnCondition(true);
                            break;
                        case 3:
                        {
                            SetWaveToSpawn(1, false);
                            break;
                        }
                        case 2:
                        case 4:
                        {
                            SetWaveToSpawn(2, true);
                            break;
                        }
                        case 5:
                            currentWave = 1;
                            GyrussGameManager.Instance.SetLevelState(LevelState.wait);
                            GyrussGameManager.Instance.SetWaveSpawnCondition(false);
                            break;
                    }

                // Debug.LogWarning("Wave created");
                
                break;
            
            case LevelState.end:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetWaveToSpawn(int enemyType, bool isWaveEven)
   {
       Wave waveToAdd = new Wave(enemyName + enemyType, isWaveEven);
       GyrussGameManager.Instance.EnqueueWave(waveToAdd);
       GyrussGameManager.Instance.SetStageState(StageState.loading_wave);
       GyrussGameManager.Instance.SetLevelState(LevelState.wait);
       currentWave++;
   }


    private void SetCurrentStageType()
   {
       int currentStage = GyrussGameManager.Instance.StageManager.CurrentStage;
       StageType currentStageType = StageType.no_type;
       
       switch (currentStage) {
           case 1:
               currentStageType = StageType.normal;
               break;
           case 2:
               currentStageType = StageType.mini_boss;
               break;
           case 3:
               currentStageType = StageType.boss; 
               break;
           case 4:
               currentStageType = StageType.chance;
               break;
       }

       GyrussGameManager.Instance.SetCurrentStageType(currentStageType);
   }

    private void SetLevelState(LevelState newLevelState)
    {
        currentLevelState = newLevelState;
    }

    public int CurrentLevel => currentLevel;

    // method to go to another level, should be added to event (delegate)
}
