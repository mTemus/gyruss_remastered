using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject BossPrefabL0;
    [SerializeField] private GameObject BossPrefabL1;

    private int bossModulesLeft = 0;
    private int currentLevel = 0;

    private int currentAlpha = 0;
    
    private GameObject currentBoss;
    private SpriteRenderer currentBossBodyRenderer;
    
    void Start()
    {
        SetDelegates();
    }

    void Update()
    {
        
    }

    private void SetDelegates()
    {
        GyrussEventManager.BossVisibilityIncreaseInitiated += IncreaseBossVisibility;
        GyrussEventManager.BossVisibilityDecreaseInitiated += DecreaseBossVisibility;
        GyrussEventManager.CurrentLevelIncreaseInitiated += IncreaseCurrentLevel;
        GyrussEventManager.BossSpawnInitiated += SpawnBoss;
    }

    private void SpawnBoss()
    {
        GyrussGameManager.Instance.TogglePlayerSpawned();
        
        switch (currentLevel) {
            case  0:
                currentBoss = Instantiate(BossPrefabL0);
                currentBoss.transform.position = new Vector3(100, 100, 0);
                currentBossBodyRenderer = currentBoss.transform.GetChild(0).GetComponent<SpriteRenderer>();
                bossModulesLeft = 4;
                
                IncreaseBossVisibility();
                currentBoss.transform.position = Vector3.zero;
                break;
        }
    }

    private void KillBossModule()
    {
        bossModulesLeft--;


        if (bossModulesLeft == 0) {
            // start spamming explosions
            // start increasing gui visibility
            // start decreasing boss visibility
            
            GyrussGameManager.Instance.TogglePlayerSpawned();
            DecreaseBossVisibility();
        }
    }
    
    private void IncreaseBossVisibility()
    {
        Color bossBodyColor = currentBossBodyRenderer.color;
        bossBodyColor.a = currentAlpha++;

        if (bossBodyColor.a == 51) {
            // decrease visibility of gui
        }
        
        if (bossBodyColor.a == 255) {
            currentAlpha = 0;
            currentBoss.transform.GetChild(1).gameObject.SetActive(true);
            currentBoss.transform.GetChild(2).gameObject.SetActive(true);
            currentBoss.transform.GetChild(3).gameObject.SetActive(true);
            currentBoss.transform.GetChild(4).gameObject.SetActive(true);
            GyrussGameManager.Instance.TogglePlayerSpawned();
        }
        else {
            GyrussGameManager.Instance.SetConditionInTimer("bossVisibilityIncrease", true);
        }
    }

    private void DecreaseBossVisibility()
    {
        Color bossBodyColor = currentBossBodyRenderer.color;
        bossBodyColor.a = 255 - currentAlpha++;

        if (bossBodyColor.a == 204) {
            // increase visibility of gui
        }

        if (bossBodyColor.a == 0) {
            GyrussGameManager.Instance.TogglePlayerSpawned();
            Destroy(currentBoss);
            currentBoss = null;
            GyrussGameManager.Instance.AddPointsToScore(25000);
        }
        else {
            GyrussGameManager.Instance.SetConditionInTimer("bossVisibilityDecrease", true);
        }
    }

    private void IncreaseCurrentLevel()
    {
        currentLevel++;
    }
}
