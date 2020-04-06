using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Game views")] 
    [SerializeField] private GameObject MinimapView;
    [SerializeField] private GameObject StageView;
    
    private int currentLevel = 0;
    private int currentWave = 1;

    private String enemyName;

    private LevelState currentLevelState = LevelState.start;

    private void Start()
    {
        SetDelegates();
    }

    private void Update()
    {
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
                // something will be there probably like loading the best score
                
                // initialize timer
                GyrussGameManager.Instance.SetConditionInTimer("playerDelayOnMinimap", true);
                
                currentLevelState = LevelState.wait;
                break;
            
            case LevelState.move_on_minimap:
                GyrussGameManager.Instance.MoveToLevelOnMinimap(currentLevel);
                currentLevelState = LevelState.wait;
                break;
            
            case LevelState.initialize_GUI:

                currentLevelState = LevelState.change_view_to_stage;
                break;
            
            case LevelState.change_view_to_stage:
                ToggleViews();
                // currentLevelState = LevelState.initialize_GUI;
                currentLevelState = LevelState.spawn_player;
                break;

            case LevelState.spawn_player:
                GyrussGameManager.Instance.PrepareReviveParticles();
                // spawn score amount
                // spawn hi-score amount
                // after ship will be spawned
                
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
                            GyrussGameManager.Instance.SetConditionInTimer("waveCreating", true);
                            break;
                        
                        case 3:
                            SetWaveToSpawn(1, false);
                            GyrussGameManager.Instance.SetConditionInTimer("waveCreating", true);
                            break;
                        
                        case 2:
                        case 4:
                            SetWaveToSpawn(2, true);
                            GyrussGameManager.Instance.SetConditionInTimer("waveCreating", true);
                            break;
                        
                        case 5:
                            //TODO: change this for chance stage
                            currentWave = 1;
                            GyrussGameManager.Instance.SetLevelState(LevelState.wait);
                            break;
                    }
                break;
            
            case LevelState.change_view_to_minimap:
                break;
            
            case LevelState.end:
                // move player ship to starting position
                // activate warping
                
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

    private void ToggleViews()
    {
        MinimapView.SetActive(!MinimapView.activeSelf);
        StageView.SetActive(!StageView.activeSelf);
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

    //TODO: method to go to another level, should be added to event (delegate)
}
