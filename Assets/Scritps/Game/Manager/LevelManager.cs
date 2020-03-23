using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   private int currentLevel = 0;
   private int currentWave = 1;

   private float waveLoadingTimer = 0;
   private float waveLoadTime = 30f;

   private String enemyName;

   private void Update()
   {
         // if current state == wait => return
         
         InitiateLevelOneStageOne();
         

   }


   private void InitiateLevelOneStageOne()
   {
      //TODO: add State machine to level manager
      //TODO: add State machine to waves creating process or no?
      
      if (waveLoadingTimer == 0) {
          int currentStage = GyrussGameManager.Instance.StageManager.CurrentStage;
          enemyName = "Enemy_L" + currentLevel + "_S" + currentStage + "_T";
          
          switch (currentWave) {
              case 1:
              case 3:
              {
                  Wave waveToAdd = new Wave(enemyName + "1");
                  GyrussGameManager.Instance.StageManager.AddNewWave(waveToAdd);
                  currentWave++;
                  break;
              }
              case 2:
              case 4:
              {
                  Wave waveToAdd = new Wave(enemyName + "2");
                  GyrussGameManager.Instance.StageManager.AddNewWave(waveToAdd);
                  currentWave++;
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
   


   // method to go to another level, should be added to event (delegate)
   
   
   
}
