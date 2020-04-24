using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int hiScore;

    private int livesGet;
    private int shipsKilledInChance;
    
    private int rocketPointPeriod;
    private int lifePointPeriod;
    private int bonusPointsForWave = 1000;
    private int perfectChanceBonus = 20000;
    
    
    private bool noChanceBonus;

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
        GyrussEventManager.EnemyInChanceStageKillInitiated += KillEnemyInChanceStage;
        GyrussEventManager.ChanceBonusCountStartInitiated += StartCountingChanceBonus;
        GyrussEventManager.ChanceBonusPointsToScoreAddingInitiated += AddBonusChancePointsToScore;
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

                if (enemiesKilledInWave.Count != 8) return;
                
                enemiesKilledInWave = new List<EnemyController>();

                GyrussGameManager.Instance.ShowKillingBonusText(bonusPointsForWave);

                bonusPointsForWave *= 2;
                if (bonusPointsForWave > 8000) bonusPointsForWave = 1000;
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

    private void KillEnemyInChanceStage()
    {
        shipsKilledInChance++;
    }

    private void ClearStage()
    {
        enemiesKilledInWave = new List<EnemyController>();
    }

    private void StartCountingChanceBonus()
    {
        GyrussGameManager.Instance.SetChanceBonusText(shipsKilledInChance);

        if (shipsKilledInChance == 0) {
            noChanceBonus = true;
        }
        
    }

    private void AddBonusChancePointsToScore()
    {
        if (shipsKilledInChance < 40) {
            switch (shipsKilledInChance) {
                case 0 when !noChanceBonus:
                    GyrussGameManager.Instance.ToggleChanceBonusText();
                    return;
            
                case 0 when noChanceBonus:
                    GyrussGameManager.Instance.SetConditionInTimer("nextStageDelay", true);
                    noChanceBonus = false;
                    return;
            
                case 10 when !noChanceBonus:
                    GyrussGameManager.Instance.SetConditionInTimer("nextStageDelay", true);
                    break;
            }
            
            GyrussGameManager.Instance.PlaySoundEffect("bonusScoreCount");
            AddPoints(100);
            shipsKilledInChance--;
            GyrussGameManager.Instance.SetConditionInTimer("chanceBonusPointsCountingTimer", true);
        }
        else {
            switch (perfectChanceBonus) {
                case 0 when !noChanceBonus:
                    GyrussGameManager.Instance.ToggleChanceBonusText();
                    perfectChanceBonus = 20000;
                    return;
            
                case 0 when noChanceBonus:
                    GyrussGameManager.Instance.SetConditionInTimer("nextStageDelay", true);
                    perfectChanceBonus = 20000;
                    noChanceBonus = false;
                    return;
            
                case 10000 when !noChanceBonus:
                    GyrussGameManager.Instance.SetConditionInTimer("nextStageDelay", true);
                    break;
            }
            
            AddPoints(100);
            perfectChanceBonus -= 100;
            GyrussGameManager.Instance.SetConditionInTimer("chanceBonusPointsCountingTimer", true);
        }
    }
    
    private void OnDestroy()
    {
        if (hiScore <= score) {
            PlayerPrefs.SetInt("hiScore", hiScore);
        } 
    }
}
