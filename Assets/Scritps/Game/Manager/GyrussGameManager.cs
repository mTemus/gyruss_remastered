﻿using UnityEngine;

public class GyrussGameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private MinimapManager minimapManager;
    [SerializeField] private EffectsManager effectsManager;

    private static GyrussGameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetCurrentStageType(StageType newStageType)
    {
        GyrussEventManager.OnStageTypeSetupInitiated(newStageType);
    }

    public void EnqueueWave(Wave wave)
    {
        GyrussEventManager.OnWaveEnqueuingInitiated(wave);
    }

    public void SetStageState(int newStageStateOnInt)
    {
        StageState newStageState = StageState.wait;


        switch (newStageStateOnInt) {
            case 0:
                newStageState = StageState.get_ready;
                break;
            
            case 1:
                newStageState = StageState.start;
                break;
            
            case 2:
                newStageState = StageState.end;
                break;

            case 3:
                newStageState = StageState.wait;
                break;

            case 4:
                newStageState = StageState.loading_wave;
                break;

            case 5:
                newStageState = StageState.spawn_enemies;
                break;
            
            case 6:
                newStageState = StageState.initialize_GUI;
                break;

            case 7:
                newStageState = StageState.spawn_player;
                break;
            
            
            default:
                Debug.LogError("GGM -- No integer to set stage state!");
                break;
        }
        GyrussEventManager.OnStageStateSetupInitiated(newStageState);
    }
    
    public void SetStageState(StageState newStageState)
    {
        GyrussEventManager.OnStageStateSetupInitiated(newStageState);
    }

    public void KillEnemy()
    {
        GyrussEventManager.OnEnemyDeathInitiated();
    }

    public Transform OccupyEnemySpot(int index) => 
        GyrussEventManager.OnEnemySpotOccupationInitiated(index);
    
    public void SetLevelState(int stateOnInt)
    {
        LevelState newLevelState = LevelState.wait;

        switch (stateOnInt) {
            case 0:
                newLevelState = LevelState.start;
                break;
            
            case 1:
                newLevelState = LevelState.move_on_minimap;
                break;
            
            case 2:
                newLevelState = LevelState.change_view_to_stage;
                break;
            
            case 3:
                newLevelState = LevelState.change_view_to_minimap;
                break;

            case 4:
                newLevelState = LevelState.wait;
                break;
            
            case 5:
                newLevelState = LevelState.create_wave;
                break;
            
            case 6:
                newLevelState = LevelState.end;
                break;
            
            default:
                Debug.LogError("GGM -- No integer to set level state!");
                break;
        }
        
        GyrussEventManager.OnLevelStateSetupInitiated(newLevelState);
    }

    public void SetLevelState(LevelState newLevelState)
    {
        GyrussEventManager.OnLevelStateSetupInitiated(newLevelState);
    }

    public void SetCurrentWave(int currentWave)
    {
        GyrussEventManager.OnCurrentWaveSetupInitiated(currentWave);
    }

    public void MoveToLevelOnMinimap(int levelIndex)
    {
        GyrussEventManager.OnMoveToLevelOnMinimapInitiated(levelIndex);
    }

    public void SetPlayerShipPosition(Vector3 playerShipPosition)
    {
        GyrussEventManager.OnPlayerShipPositionSetupInitiated(playerShipPosition);
    }

    public void RegisterReviveParticle(GameObject reviveParticle)
    {
        GyrussEventManager.OnReviveParticleRegistrationInitiated(reviveParticle);
    }

    public void PrepareReviveParticles()
    {
        GyrussEventManager.OnReviveParticlesPreparationInitiated();
    }

    public Vector3 GetPlayerShipPosition()
    {
        return GyrussEventManager.OnGetPlayerShipPositionInitiated();
    }

    public void SetPlayerEnteredInAnimator(bool entered)
    {
        GyrussEventManager.OnPlayerEnteredSetupInAnimatorInitiated(entered);
    }

    public void SpawnPlayerShip()
    {
        GyrussEventManager.OnPlayerShipSpawnInitiated();
    }

    public void SetConditionInTimer(string timerName, bool timerCondition)
    {
        GyrussEventManager.OnConditionSetupInTimerInitiated(timerName, timerCondition);
    }

    public void SetPeriodInTimer(string timerName, float timerPeriod)
    {
        GyrussEventManager.OnPeriodSetupInTimerInitiated(timerName, timerPeriod);
    }

    public void StopTimer(string timerName)
    {
        GyrussEventManager.OnTimerStopInitiated(timerName);
    }

    public void ResetPeriodInTimer(string timerName)
    {
        GyrussEventManager.OnPeriodResetInTimerInitiated(timerName);
    }

    public void ToggleWarpsText()
    {
        GyrussGUIEventManager.OnWarpsTextToggleInitiated();
    }

    public void ToggleReadyText()
    {
        GyrussGUIEventManager.OnReadyTextToggleInitiated();
    }

    public void ToggleScoreText()
    {
        GyrussGUIEventManager.OnScoreTextToggleInitiated();
    }

    public void SetWarpsText(int warps, string planet)
    {
        GyrussGUIEventManager.OnWarpsTextSetupInitiated(warps, planet);
    }

    public int GetPlayerLives() => 
        GyrussEventManager.OnPlayerLivesGetInitiated();

    public int GetPlayerRockets() => 
        GyrussEventManager.OnPlayerRocketsGetInitiated();

    public void InitializeLifeIcons()
    {
        GyrussGUIEventManager.OnLifeIconsInitializeInitiated();
    }

    public void InitializeRocketIcons()
    {
        GyrussGUIEventManager.OnRocketIconsInitializeInitiated();
    }

    public void CreateExplosion(Vector3 explosionPosition)
    {
        GyrussEventManager.OnExplosionCreationInitiated(explosionPosition);
    }

    public void TogglePlayerSpawned()
    {
        GyrussEventManager.OnPlayerSpawnedToggleInitiated();
    }

    public bool MovePlayerShipToWarpingPosition() => GyrussEventManager.OnMovePlayerToWarpPositionInitiated();

    public void ToggleWarpingEffects()
    {
        GyrussEventManager.OnWarpingEffectsToggleInitiated();
    }

    public void WarpPlayer()
    {
        GyrussEventManager.OnWarpingPlayerInitiated();
    }

    public bool MovePlayerShipToCenterPosition() => GyrussEventManager.OnMovePlayerToCenterPointInitiated();

    public void GoToNextStage()
    {
        GyrussEventManager.OnGoToNextStageInitiated();
    }

    public void SpawnAsteroid()
    {
        GyrussEventManager.OnAsteroidSpawnInitiated();
    }
    
    
    private void OnDestroy()
    {
        GyrussEventManager.ClearDelegates();
    }
    
    public static GyrussGameManager Instance => instance;
    
    public PlayerManager PlayerManager => playerManager;

    public StageManager StageManager => stageManager;
    
    public LevelManager LevelManager => levelManager;

    public TimeManager TimeManager => timeManager;

    public MinimapManager MinimapManager => minimapManager;

    public EffectsManager EffectsManager => effectsManager;
}
