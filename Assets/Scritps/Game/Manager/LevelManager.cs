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

                currentLevelState = LevelState.create_wave;
                
                break;
            case LevelState.wait:
                
                break;
            case LevelState.create_wave:
                //TODO: add State machine to waves creating process or no?
      
                if (waveLoadingTimer == 0) {
                    int currentStage = GyrussGameManager.Instance.StageManager.CurrentStage;
                    enemyName = "Enemy_L" + currentLevel + "_S" + currentStage + "_T";
          
                    switch (currentWave) {
                        case 1:
                            SetCurrentStageType();
                            SetWaveToSpawn(1);
                            break;
                        case 3:
                        {
                            SetWaveToSpawn(1);
                            break;
                        }
                        case 2:
                        case 4:
                        {
                            SetWaveToSpawn(2);
                            break;
                        }
                        case 5:
                            currentLevelState = LevelState.wait;
                            break;
                    }
                }
                //TODO: move timer to time manager
                else {
                    waveLoadingTimer += Time.deltaTime;
                    if (waveLoadingTimer >= waveLoadTime) waveLoadingTimer = 0; 
                }
                
                break;
            
            case LevelState.end:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetWaveToSpawn(int enemyType)
   {
       Wave waveToAdd = new Wave(enemyName + enemyType);
       GyrussGameManager.Instance.EnqueueWave(waveToAdd);
       
       GyrussGameManager.Instance.SetStageState(StageState.loading_wave);
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
