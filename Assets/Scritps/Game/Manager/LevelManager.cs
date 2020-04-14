﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Game views")] 
    [SerializeField] private GameObject MinimapView;
    [SerializeField] private GameObject StageView;
    
    private int currentLevel = 0;
    private int currentWave = 1;
    private int currentStage = 1;

    private List<string> planetsInGame;

    private String enemyName;

    private LevelState currentLevelState = LevelState.start;

    private void Start()
    {
        planetsInGame = new List<string> {"NEPTUNE", "PLUTO"};
        SetDelegates();
    }

    private void Update()
    {
        ProcessOrdersInLevel();
    }

    private void SetDelegates()
    {
        GyrussEventManager.LevelStateSetupInitiated += SetLevelState;
        GyrussEventManager.CurrentStageSetupInitiated += SetCurrentStageNumber;
    }

    private void ProcessOrdersInLevel()
    {
        switch (currentLevelState) {
            case LevelState.start:
                if (MinimapView.activeSelf) {
                    GyrussGameManager.Instance.SetConditionInTimer("playerDelayOnMinimap", true);
                    currentLevelState = LevelState.wait;
                }
                else {
                    GyrussGameManager.Instance.SetLevelState(LevelState.change_view_to_stage);
                }
                break;
            
            case LevelState.move_on_minimap:
                GyrussGameManager.Instance.MoveToLevelOnMinimap(currentLevel);
                currentLevelState = LevelState.wait;
                break;
            
            case LevelState.change_view_to_stage:

                if (!StageView.activeSelf) ToggleViews();

                if (currentStage < 4)
                    GyrussGameManager.Instance.SetWarpsText(4 - currentStage, planetsInGame[currentLevel]);
                
                GyrussGameManager.Instance.SetConditionInTimer("warpsTextDelay", true);
                GyrussGameManager.Instance.SetConditionInTimer("readyTextDelay", true);

                currentLevelState = LevelState.wait;
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
               currentStageType = StageType.first_stage;
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

    private void SetCurrentStageNumber(int stage)
    {
        currentStage = stage;
    }

    public int CurrentLevel => currentLevel;

    //TODO: method to go to another level, should be added to event (delegate)
}
