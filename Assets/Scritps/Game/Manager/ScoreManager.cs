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


    private void Start()
    {
        SetDelegates();
        hiScore = PlayerPrefs.GetInt("hiScore");
        GyrussGameManager.Instance.SetHiScorePoints(hiScore);
    }

    private void SetDelegates()
    {
        GyrussEventManager.ScorePointsIncreaseInitiated += AddPoints;
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

    private void SaveHiScore()
    {
        PlayerPrefs.SetInt("hiScore", hiScore);
    }
    
    private void OnDestroy()
    {
        if (hiScore <= score) {
            PlayerPrefs.SetInt("hiScore", hiScore);
        }
    }
}
