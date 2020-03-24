using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel = 0;
    private int currentWave = 1;

    private String enemyName;

    private float waveLoadingTimer = 0;
    private float waveLoadTime = 30f;

    private void Update()
   {
         // if current state == wait => return
         
         InitiateWaves();
         

   }


    private void InitiateWaves()
   {
      //TODO: add State machine to level manager
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
                  return;
          }
      }
      else {
          waveLoadingTimer += Time.deltaTime;
          if (waveLoadingTimer >= waveLoadTime) waveLoadingTimer = 0; 
      }
   }

    private void SetWaveToSpawn(int enemyType)
   {
       Wave waveToAdd = new Wave(enemyName + enemyType);
       GyrussGameManager.Instance.StageManager.AddNewWave(waveToAdd);
       currentWave++;
   }


    private void SetCurrentStageType()
   {
       int currentStage = GyrussGameManager.Instance.StageManager.CurrentStage;
       StageType currentStageType;
       
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

   }


    // method to go to another level, should be added to event (delegate)
}
