using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int hiScore;

    private int livesGet;
    
    private int rocketPointPeriod;
    private int lifePointPeriod;
    private int bonusPointsForWave = 1000;


    private List<EnemyController> enemiesKilledInWave;

    
    private void Start()
    {
        enemiesKilledInWave = new List<EnemyController>();
        
        SetDelegates();
        hiScore = PlayerPrefs.GetInt("hiScore");
        GyrussGameManager.Instance.SetHiScorePoints(hiScore);
    }

    private void SetDelegates()
    {
        GyrussEventManager.ScorePointsIncreaseInitiated += AddPoints;
        GyrussEventManager.BonusPointsForWaveKillInitiated += CheckBonusForWave;
        GyrussEventManager.StageClearInitiated += ClearStage;
    }

    private void AddPoints(int additionalPoints)
    {
        score += additionalPoints;
        
        GyrussGameManager.Instance.SetScorePoints(score);

        if (score > hiScore) {
            hiScore = score;
            GyrussGameManager.Instance.SetHiScorePoints(hiScore);
        }
        
        CheckScoreBonus();
    }

    private void CheckScoreBonus()
    {
        switch (livesGet) {
            case 0:
                if (score > 50000) {
                    GyrussGameManager.Instance.AddPlayerLife();
                    livesGet++;
                }
                break;
            
            case 1: 
                if (score > 100000) {
                    GyrussGameManager.Instance.AddPlayerLife();
                    livesGet++;
                }
                break;
            
            case 2: 
                if (score > 150000) {
                    GyrussGameManager.Instance.AddPlayerLife();
                    livesGet++;
                }
                break;
            
            default:
                return;
        }
    }

    private void CheckBonusForWave(EnemyController eC)
    {
        if (enemiesKilledInWave.Count > 1) {
            int eCid = enemiesKilledInWave.Count - 1;
            if (enemiesKilledInWave[eCid].WaveId == eC.WaveId) {
                enemiesKilledInWave.Add(eC);

                if (enemiesKilledInWave.Count == 8) {
                    enemiesKilledInWave = new List<EnemyController>();
                    
                    
                    Debug.LogError("bonus");
                    
                    GyrussGameManager.Instance.ShowKillingBonusText(bonusPointsForWave);

                    bonusPointsForWave *= 2;
                    if (bonusPointsForWave > 8000) bonusPointsForWave = 1000; 
                }
            }
            else {
                enemiesKilledInWave = new List<EnemyController> {eC};
                bonusPointsForWave = 1000;
            }
        }
        else {
            enemiesKilledInWave.Add(eC);
        }
    }

    public void ClearStage()
    {
        enemiesKilledInWave = new List<EnemyController>();
    }
    
    private void OnDestroy()
    {
        if (hiScore <= score) {
            PlayerPrefs.SetInt("hiScore", hiScore);
        } 
    }
}
