using UnityEngine;
using PathCreation;

public class GyrussGameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager = null;
    [SerializeField] private LevelManager levelManager = null;
    [SerializeField] private StageManager stageManager = null;
    [SerializeField] private TimeManager timeManager = null;
    [SerializeField] private MinimapManager minimapManager = null;
    [SerializeField] private EffectsManager effectsManager = null;
    [SerializeField] private ScoreManager scoreManager = null;
    [SerializeField] private EnemyManager enemyManager = null;
    [SerializeField] private BossManager bossManager = null;
    
    private static GyrussGameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void ToggleEngineTime()
    {
        switch (Time.timeScale) {
            case 1:
                Time.timeScale = 0;
                break;
            case 0:
                Time.timeScale = 1;
                break;
        }
    }
    
    public void SetGameStatusGameOver()
    {
        DeleteAllEnemies();
        StopCurrentPlayingBGM();
        PlayBGM("gameOver");
        DisplayGameOverText();
    }

    public void PauseGame()
    {
        ToggleCurrentPlayingBGM();
        TogglePauseText();
        ToggleEngineTime();
    }

    public void AskIfExitGame()
    {
        ToggleCurrentPlayingBGM();
        ToggleExitPanel();
        ToggleEngineTime();
    }

    public void QuitGame()
    {
        Application.Quit();
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

    public void CreateExplosion(Vector3 explosionPosition, string explosionType)
    {
        GyrussEventManager.OnExplosionCreationInitiated(explosionPosition, explosionType);
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

    public void SetDeathParticlesOnPosition()
    {
        GyrussEventManager.OnDeathParticlesOnPositionsSetupInitiated();
    }

    public void DecreaseGUILives(int livesLeft)
    {
        GyrussGUIEventManager.OnLivesIconsDecreaseInitiated(livesLeft);
    }
    
    public void PrepareDeathParticles()
    {
        GyrussEventManager.OnDeathParticlesPreparationInitiated();
    }

    public void KillPlayer()
    {
        GyrussEventManager.OnPlayerKillInitiated();
    }

    public void SetStagesText(int stages)
    {
        GyrussGUIEventManager.OnStagesTextSetupInitiated(stages);
    }

    public void SetRocketParticlesOnPosition()
    {
        GyrussEventManager.OnRocketParticlesOnPositionsSetupInitiated();
    }
    
    public void DecreasePlayerRockets(int rockets)
    {
        GyrussGUIEventManager.OnRocketsIconsDecreaseInitiated(rockets);
    }

    public void ShootRocket()
    {
        GyrussEventManager.OnRocketShootInitiated();
    }

    public void ReloadRocket()
    {
        GyrussEventManager.OnRocketReloadInitiated();
    }

    public void PrepareRocketParticle()
    {
        GyrussEventManager.OnRocketParticlesPreparationInitiated();
    }

    public void SetScorePoints(int score)
    {
        GyrussGUIEventManager.OnScoreTextSetupInitiated(score);
    }

    public void SetHiScorePoints(int hiScore)
    {
        GyrussGUIEventManager.OnHiScoreTextSetupInitiated(hiScore);
    }

    public void IncreaseLifeIcons()
    {
        GyrussGUIEventManager.OnLivesIconsIncreaseInitiated();
    }

    public void AddPlayerLife()
    {
        GyrussEventManager.OnLifeAddInitiated();
    }

    public void AddPointsToScore(int score)
    {
        GyrussEventManager.OnScorePointsIncreaseInitiated(score);
    }

    public void CheckBonusPointsForWaveKill(EnemyController eC)
    {
        GyrussEventManager.OnBonusPointsForWaveKillInitiated(eC);
    }

    public void ClearCurrentStage()
    {
        GyrussEventManager.OnStageClearInitiated();
    }

    public void ShowKillingBonusText(int score)
    {
        GyrussGUIEventManager.OnWaveBonusTextShowInitiated(score);
    }

    public void SetCurrentStageNumber(int stage)
    {
        GyrussEventManager.OnCurrentStageSetupInitiated(stage);
    }

    public void KillMiniBossModule(GameObject module)
    {
        GyrussEventManager.OnMiniBossModuleKillInitiated(module);
    }

    public void RemoveShipFromAwaitingList(GameObject module, GameObject ship)
    {
        GyrussEventManager.OnShipRemovalFromAwaitingListInitiated(module, ship);
    }

    public void IncreaseBossVisibility()
    {
        GyrussEventManager.OnBossVisibilityIncreaseInitiated();
    }

    public void DecreaseBossVisibility()
    {
        GyrussEventManager.OnBossVisibilityDecreaseInitiated();
    }

    public void IncreaseCurrentLevel()
    {
        GyrussEventManager.OnCurrentLevelIncreaseInitiated();
    }

    public void SpawnBoss()
    {
        GyrussEventManager.OnBossSpawnInitiated();
    }

    public void IncreaseGUIVisibility()
    {
        GyrussGUIEventManager.OnGUIVisibilityIncreaseInitiated();
    }

    public void DecreaseGUIVisibility()
    {
        GyrussGUIEventManager.OnGUIVisibilityDecreaseInitiated();
    }

    public void KillBossModule()
    {
        GyrussEventManager.OnBossModuleKillInitiated();
    }

    public void CreateBossExplosion()
    {
        GyrussEventManager.OnBossExplosionInitiated();
    }

    public void DisplayPlanet(int planetId)
    {
        GyrussGUIEventManager.OnPlanetDisplayInitiated(planetId);
    }

    public void DestroyPlanet()
    {
        GyrussGUIEventManager.OnPlanetDestroyInitiated();
    }

    public void BlinkChanceStageText()
    {
        GyrussGUIEventManager.OnChanceTextBlinkInitiated();
    }

    public void DisplayChanceStageText()
    {
        GyrussGUIEventManager.OnChanceStageTextDisplayInitiated();
    }

    public void KillEnemyInChanceStage()
    {
        GyrussEventManager.OnEnemyInChanceStageKillInitiated();
    }

    public void StartCountingChanceBonusPoints()
    {
        GyrussEventManager.OnChanceBonusCountStartInitiated();
    }

    public void SetChanceBonusText(int shipsKilled)
    {
        GyrussGUIEventManager.OnChanceBonusScoreDisplayInitiated(shipsKilled);
    }

    public void AddChanceBonusPointsToScore()
    {
        GyrussEventManager.OnChanceBonusPointsToScoreAddingInitiated();
    }

    public void BlinkChanceBonusText()
    {
        GyrussGUIEventManager.OnChanceBonusTextBlinkInitiated();
    }

    public void ToggleChanceBonusText()
    {
        GyrussGUIEventManager.OnToggleChanceBonusTryInitiated();
    }

    public void ToggleStarParticles()
    {
        GyrussEventManager.OnStarParticlesToggleInitiated();
    }

    public void PlaySoundEffect(string effectName)
    {
        GyrussEventManager.OnSoundEffectPlayInitiated(effectName);
    }

    public void PlayBGM(string BGMName)
    {
        GyrussEventManager.OnSoundBGMPlayInitiated(BGMName);
    }

    public void StopBGM(string BGMName)
    {
        GyrussEventManager.OnSoundBGMStopInitiated(BGMName);
    }

    public void StopCurrentPlayingBGM()
    {
        GyrussEventManager.OnCurrentSoundBGMStopInitiated();
    }

    public bool IsBGMPlaying() =>
        GyrussEventManager.OnIsBGMPlayingInitiated();

    public void SilenceCurrentPlayingBGM()
    {
        GyrussEventManager.OnCurrentPlayingBGMSilencingInitiated();
    }

    public void DisplayGameOverText()
    {
        GyrussGUIEventManager.OnGameOverTextDisplayInitiated();
    }

    public void DeleteAllEnemies()
    {
        GyrussEventManager.OnAllEnemiesDeleteInitiated();
    }

    public void CountToRestartGame()
    {
        GyrussGUIEventManager.OnGameRestartCountInitiated();
    }

    public void DisplayGameEnding()
    {
        GyrussGUIEventManager.OnGameEndingDisplayInitiated();
    }

    public void ToggleShootingMode()
    {
        GyrussEventManager.OnShootingModeToggleInitiated();
    }

    public void AddRocket()
    {
        GyrussEventManager.OnRocketAddInitialized();
    }

    public void SpawnBonus(string bonusType)
    {
        GyrussEventManager.OnBonusSpawnInitiated(bonusType);
    }

    public Vector3 GetPlayerStartingPosition() => 
        GyrussEventManager.OnGetPlayerShipStartingPositionInitiated();

    public void SetEnemyPathIn(){
        EnemyManager.setPathIn();
    }

    public PathCreator GetCurrentEnemyPathIn(){
        return EnemyManager.getCurrentPath();
    }
    public void SetEnemyPathOut(){
        EnemyManager.setPathOut();
    }

    public PathCreator GetCurrentEnemyPathOut(){
        return EnemyManager.getCurrentPathOut();
    }

    public void setClosestPathOut(){
        EnemyManager.setClosestPathOut();
    }

    public PathCreator getClosestPathOut(){
        return EnemyManager.getClosestPathOut();
    }

    public void KillWeaponBonus()
    {
        GyrussEventManager.OnWeaponBonusKillInitiated();
    }

    public void TogglePauseText()
    {
        GyrussGUIEventManager.OnPausedTextToggleInitiated();
    }

    public void ToggleExitPanel()
    {
        GyrussGUIEventManager.OnExitPanelToggleInitiated();
    }

    public void ToggleCurrentPlayingBGM()
    {
        GyrussEventManager.OnCurrentBGMToggleInitiated();
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

    public ScoreManager ScoreManager => scoreManager;

    public BossManager BossManager => bossManager;

    public EnemyManager EnemyManager => enemyManager;
}

