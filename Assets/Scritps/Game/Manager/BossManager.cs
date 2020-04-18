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

    private float currentAlpha = 0;
    
    private GameObject currentBoss;
    private SpriteRenderer currentBossBodyRenderer;
    private static readonly int Spawned = Animator.StringToHash("spawned");

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
        GyrussGameManager.Instance.StopTimer("asteroidSpawn");
        
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
        currentBossBodyRenderer.color = new Color(bossBodyColor.r, bossBodyColor.g, bossBodyColor.b, currentAlpha);
        currentAlpha += 0.20f;

        if (bossBodyColor.a == 0.20f) {
            GyrussGameManager.Instance.DecreaseGUIVisibility();
        }
        
        if (bossBodyColor.a == 1) {
            currentAlpha = 0;

            for (int i = 1; i <= bossModulesLeft; i++) {
                currentBoss.transform.GetChild(i).gameObject.SetActive(true);
            }

            currentBoss.transform.GetChild(0).GetComponent<Animator>().SetBool(Spawned, true);
            GyrussGameManager.Instance.TogglePlayerSpawned();
        }
        else {
            GyrussGameManager.Instance.SetConditionInTimer("bossVisibilityIncrease", true);
        }
    }

    private void DecreaseBossVisibility()
    {
        Color bossBodyColor = currentBossBodyRenderer.color;
        bossBodyColor.a = 1 - currentAlpha;
        currentBossBodyRenderer.color = new Color(bossBodyColor.r, bossBodyColor.g, bossBodyColor.b, currentAlpha);
        currentAlpha += 0.20f;

        if (bossBodyColor.a == 0.80f) {
            GyrussGameManager.Instance.IncreaseGUIVisibility();
        }

        if (bossBodyColor.a == 0) {
            currentAlpha = 0;
            
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
