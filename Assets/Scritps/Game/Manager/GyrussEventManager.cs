using System;
using UnityEngine;

public class GyrussEventManager : MonoBehaviour
{
    public static Func<int, Transform> EnemySpotOccupationInitiated;
    public static Func<Vector3> GetPlayerShipPositionInitiated;
    public static Func<Vector3> GetPlayerShipStartingPositionInitiated;
    public static Func<int> PlayerLivesGetInitiated;
    public static Func<int> PlayerRocketsGetInitiated;
    public static Func<bool> MovePlayerToWarpPositionInitiated;
    public static Func<bool> MovePlayerToCenterPointInitiated;
    public static Func<bool> IsBGMPlayingInitiated;
    public static Action EnemyDeathInitiated;
    public static Action ReviveParticlesPreparationInitiated;
    public static Action PlayerShipSpawnInitiated;
    public static Action PlayerSpawnedToggleInitiated;
    public static Action WarpingEffectsToggleInitiated;
    public static Action WarpingPlayerInitiated;
    public static Action GoToNextStageInitiated;
    public static Action AsteroidSpawnInitiated;
    public static Action PlayerKillInitiated;
    public static Action DeathParticlesOnPositionsSetupInitiated;
    public static Action DeathParticlesPreparationInitiated;
    public static Action RocketParticlesOnPositionsSetupInitiated;
    public static Action RocketShootInitiated;
    public static Action RocketReloadInitiated;
    public static Action RocketParticlesPreparationInitiated;
    public static Action LifeAddInitiated;
    public static Action StageClearInitiated;
    public static Action BossVisibilityIncreaseInitiated;
    public static Action BossVisibilityDecreaseInitiated;
    public static Action CurrentLevelIncreaseInitiated;
    public static Action BossSpawnInitiated;
    public static Action BossModuleKillInitiated;
    public static Action BossExplosionInitiated;
    public static Action ChanceBonusCountStartInitiated;
    public static Action EnemyInChanceStageKillInitiated;
    public static Action ChanceBonusPointsToScoreAddingInitiated;
    public static Action StarParticlesToggleInitiated;
    public static Action CurrentSoundBGMStopInitiated;
    public static Action CurrentPlayingBGMSilencingInitiated;
    public static Action AllEnemiesDeleteInitiated;
    public static Action ShootingModeToggleInitiated;
    public static Action RocketAddInitialized;
    public static Action WeaponBonusKillInitiated;
    public static Action CurrentBGMToggleInitiated;
    public static Action<StageType> StageTypeSetupInitiated;
    public static Action<StageState> StageStateSetupInitiated;
    public static Action<LevelState> LevelStateSetupInitiated;
    public static Action<Wave> WaveEnqueuingInitiated;
    public static Action<Vector3> PlayerShipPositionSetupInitiated;
    public static Action<Vector3, string> ExplosionCreationInitiated;
    public static Action<GameObject> ReviveParticleRegistrationInitiated;
    public static Action<GameObject> MiniBossModuleKillInitiated;
    public static Action<EnemyController> BonusPointsForWaveKillInitiated;
    public static Action<int> MoveToLevelOnMinimapInitiated;
    public static Action<int> CurrentWaveSetupInitiated;
    public static Action<int> ScorePointsIncreaseInitiated;
    public static Action<int> CurrentStageSetupInitiated;
    public static Action<bool> PlayerEnteredSetupInAnimatorInitiated;
    public static Action<string> BonusSpawnInitiated;
    public static Action<string> TimerStopInitiated;
    public static Action<string> PeriodResetInTimerInitiated;
    public static Action<string> SoundEffectPlayInitiated;
    public static Action<string> SoundBGMPlayInitiated;
    public static Action<string> SoundBGMStopInitiated;
    public static Action<string, bool> ConditionSetupInTimerInitiated;
    public static Action<string, float> PeriodSetupInTimerInitiated;
    public static Action<GameObject, GameObject> ShipRemovalFromAwaitingListInitiated;

    public static void OnCurrentBGMToggleInitiated()
    {
        CurrentBGMToggleInitiated?.Invoke();
    }
    
    public static void OnWeaponBonusKillInitiated()
    {
        WeaponBonusKillInitiated?.Invoke();
    }
    
    public static Vector3 OnGetPlayerShipStartingPositionInitiated() =>
        (Vector3) GetPlayerShipStartingPositionInitiated?.Invoke();
    
    public static void OnBonusSpawnInitiated(string bonusType)
    {
        BonusSpawnInitiated?.Invoke(bonusType);
    }
    
    public static void OnRocketAddInitialized()
    {
        RocketAddInitialized?.Invoke();
    }
    
    public static void OnShootingModeToggleInitiated()
    {
        ShootingModeToggleInitiated?.Invoke();
    }
    
    public static void OnAllEnemiesDeleteInitiated()
    {
        AllEnemiesDeleteInitiated?.Invoke();
    }
    
    public static void OnCurrentPlayingBGMSilencingInitiated()
    {
        CurrentPlayingBGMSilencingInitiated?.Invoke();
    }
    
    public static bool OnIsBGMPlayingInitiated() => 
        IsBGMPlayingInitiated.Invoke();
    
    public static void OnCurrentSoundBGMStopInitiated()
    {
        CurrentSoundBGMStopInitiated?.Invoke();
    }
    
    public static void OnSoundBGMStopInitiated(string name)
    {
        SoundBGMStopInitiated?.Invoke(name);
    }
    
    public static void OnSoundBGMPlayInitiated(string name)
    {
        SoundBGMPlayInitiated?.Invoke(name);
    } 
    
    public static void OnSoundEffectPlayInitiated(string name)
    {
        SoundEffectPlayInitiated?.Invoke(name);
    }
    
    public static void OnStarParticlesToggleInitiated()
    {
        StarParticlesToggleInitiated?.Invoke();
    }
    
    public static void OnChanceBonusPointsToScoreAddingInitiated()
    {
        ChanceBonusPointsToScoreAddingInitiated?.Invoke();
    }
    
    public static void OnChanceBonusCountStartInitiated()
    {
        ChanceBonusCountStartInitiated?.Invoke();
    }
    
    public static void OnEnemyInChanceStageKillInitiated()
    {
        EnemyInChanceStageKillInitiated?.Invoke();
    }
    
    public static void OnBossExplosionInitiated()
    {
        BossExplosionInitiated?.Invoke();
    }
    
    public static void OnBossModuleKillInitiated()
    {
        BossModuleKillInitiated?.Invoke();
    }
    
    public static void OnBossSpawnInitiated()
    {
        BossSpawnInitiated?.Invoke();
    }
    
    public static void OnCurrentLevelIncreaseInitiated()
    {
        CurrentLevelIncreaseInitiated?.Invoke();
    }
    
    public static void OnBossVisibilityDecreaseInitiated()
    {
        BossVisibilityDecreaseInitiated?.Invoke();
    }
    
    public static void OnBossVisibilityIncreaseInitiated()
    {
        BossVisibilityIncreaseInitiated?.Invoke();
    }
    
    public static void OnShipRemovalFromAwaitingListInitiated(GameObject module, GameObject ship)
    {
        ShipRemovalFromAwaitingListInitiated?.Invoke(module, ship);
    }
    
    public static void OnMiniBossModuleKillInitiated(GameObject module)
    {
        MiniBossModuleKillInitiated?.Invoke(module);
    }
    
    public static void OnCurrentStageSetupInitiated(int stage)
    {
        CurrentStageSetupInitiated?.Invoke(stage);
    }
    
    public static void OnStageClearInitiated()
    {
        StageClearInitiated?.Invoke();
    }
    
    public static void OnBonusPointsForWaveKillInitiated(EnemyController eC)
    {
        BonusPointsForWaveKillInitiated?.Invoke(eC);
    }
    
    public static void OnScorePointsIncreaseInitiated(int score)
    {
        ScorePointsIncreaseInitiated?.Invoke(score);
    }
    
    public static void OnLifeAddInitiated()
    {
        LifeAddInitiated?.Invoke();
    }
    
    public static void OnRocketParticlesPreparationInitiated()
    {
        RocketParticlesPreparationInitiated?.Invoke();
    }
    
    public static void OnRocketReloadInitiated()
    {
        RocketReloadInitiated?.Invoke();
    }
    
    public static void OnRocketShootInitiated()
    {
        RocketShootInitiated?.Invoke();
    }
    
    public static void OnRocketParticlesOnPositionsSetupInitiated()
    {
        RocketParticlesOnPositionsSetupInitiated?.Invoke();
    }
    
    public static void OnPlayerKillInitiated()
    {
        PlayerKillInitiated?.Invoke();
    }
    
    public static void OnDeathParticlesPreparationInitiated()
    {
        DeathParticlesPreparationInitiated?.Invoke();
    }
    
    public static void OnDeathParticlesOnPositionsSetupInitiated()
    {
        DeathParticlesOnPositionsSetupInitiated?.Invoke();
    }
    
    public static void OnAsteroidSpawnInitiated()
    {
        AsteroidSpawnInitiated?.Invoke();
    }
    
    public static void OnPeriodResetInTimerInitiated(string timerName)
    {
        PeriodResetInTimerInitiated?.Invoke(timerName);
    }
    
    public static void OnTimerStopInitiated(string timerName)
    {
        TimerStopInitiated?.Invoke(timerName);
    }

    public static void OnPeriodSetupInTimerInitiated(string timerName, float timerPeriod)
    {
        PeriodSetupInTimerInitiated?.Invoke(timerName, timerPeriod);
    }
    
    public static void OnGoToNextStageInitiated()
    {
        GoToNextStageInitiated?.Invoke();
    }
    
    public static bool OnMovePlayerToCenterPointInitiated() => 
        (bool) MovePlayerToCenterPointInitiated?.Invoke();
    
    public static void OnWarpingPlayerInitiated()
    {
        WarpingPlayerInitiated?.Invoke();
    }
    
    public static void OnWarpingEffectsToggleInitiated()
    {
        WarpingEffectsToggleInitiated?.Invoke();
    }
    
    public static bool OnMovePlayerToWarpPositionInitiated() => 
        (bool) MovePlayerToWarpPositionInitiated?.Invoke();
    
    public static void OnPlayerSpawnedToggleInitiated()
    {
        PlayerSpawnedToggleInitiated?.Invoke();
    }
    
    public static void OnExplosionCreationInitiated(Vector3 explosionPosition, string explosionType)
    {
        ExplosionCreationInitiated?.Invoke(explosionPosition, explosionType);
    }
    
    public static int OnPlayerRocketsGetInitiated() => 
        (int) PlayerRocketsGetInitiated?.Invoke();
    
    public static int OnPlayerLivesGetInitiated() => 
        (int) PlayerLivesGetInitiated?.Invoke();

    public static void OnConditionSetupInTimerInitiated(string timerName, bool timerCondition)
    {
        ConditionSetupInTimerInitiated?.Invoke(timerName, timerCondition);
    }

    public static void OnPlayerShipSpawnInitiated()
    {
        PlayerShipSpawnInitiated?.Invoke();
    }
    
    public static void OnPlayerEnteredSetupInAnimatorInitiated(bool entered)
    {
        PlayerEnteredSetupInAnimatorInitiated?.Invoke(entered);
    }
    
    public static Vector3 OnGetPlayerShipPositionInitiated() => 
        (Vector3) GetPlayerShipPositionInitiated?.Invoke();
    
    public static void OnReviveParticlesPreparationInitiated()
    {
        ReviveParticlesPreparationInitiated?.Invoke();
    }
    
    public static void OnReviveParticleRegistrationInitiated(GameObject reviveParticle)
    {
        ReviveParticleRegistrationInitiated?.Invoke(reviveParticle);
    }
    
    public static void OnPlayerShipPositionSetupInitiated(Vector3 playerShipPosition)
    {
        PlayerShipPositionSetupInitiated?.Invoke(playerShipPosition);
    }
    
    public static void OnMoveToLevelOnMinimapInitiated(int levelIndex)
    {
        MoveToLevelOnMinimapInitiated?.Invoke(levelIndex);
    }
    
    public static void OnCurrentWaveSetupInitiated(int currentWave)
    {
        CurrentWaveSetupInitiated?.Invoke(currentWave);
    }

    public static void OnLevelStateSetupInitiated(LevelState newLevelState)
    {
        LevelStateSetupInitiated?.Invoke(newLevelState);
    }
    
    public static Transform OnEnemySpotOccupationInitiated(int index) => 
        EnemySpotOccupationInitiated?.Invoke(index);
    
    public static void OnEnemyDeathInitiated()
    {
        EnemyDeathInitiated?.Invoke();
    }

    public static void OnStageTypeSetupInitiated(StageType newStageType)
    {
        StageTypeSetupInitiated?.Invoke(newStageType);
    }

    public static void OnWaveEnqueuingInitiated(Wave wave)
    {
        WaveEnqueuingInitiated?.Invoke(wave);
    }

    public static void OnStageStateSetupInitiated(StageState newStageState)
    {
        StageStateSetupInitiated?.Invoke(newStageState);
    }
    
    public static void ClearDelegates()
    {
        StageTypeSetupInitiated = null;
        WaveEnqueuingInitiated = null;
        StageStateSetupInitiated = null;
        EnemyDeathInitiated = null;
        EnemySpotOccupationInitiated = null;
        LevelStateSetupInitiated = null;
        CurrentWaveSetupInitiated = null;
        MoveToLevelOnMinimapInitiated = null;
        PlayerShipPositionSetupInitiated = null;
        ReviveParticleRegistrationInitiated = null;
        ReviveParticlesPreparationInitiated = null;
        GetPlayerShipPositionInitiated = null;
        PlayerEnteredSetupInAnimatorInitiated = null;
        PlayerShipSpawnInitiated = null;
        ConditionSetupInTimerInitiated = null;
        PlayerLivesGetInitiated = null;
        PlayerRocketsGetInitiated = null;
        ExplosionCreationInitiated = null;
        PlayerSpawnedToggleInitiated = null;
        MovePlayerToWarpPositionInitiated = null;
        WarpingEffectsToggleInitiated = null;
        WarpingPlayerInitiated = null;
        MovePlayerToCenterPointInitiated = null;
        GoToNextStageInitiated = null;
        PeriodSetupInTimerInitiated = null;
        TimerStopInitiated = null;
        PeriodResetInTimerInitiated = null;
        AsteroidSpawnInitiated = null;
        DeathParticlesOnPositionsSetupInitiated = null;
        DeathParticlesPreparationInitiated = null;
        PlayerKillInitiated = null;
        RocketParticlesOnPositionsSetupInitiated = null;
        RocketShootInitiated = null;
        RocketReloadInitiated = null;
        RocketParticlesPreparationInitiated = null;
        LifeAddInitiated = null;
        ScorePointsIncreaseInitiated = null;
        BonusPointsForWaveKillInitiated = null;
        StageClearInitiated = null;
        CurrentStageSetupInitiated = null;
        MiniBossModuleKillInitiated = null;
        ShipRemovalFromAwaitingListInitiated = null;
        BossVisibilityIncreaseInitiated = null;
        BossVisibilityDecreaseInitiated = null;
        CurrentLevelIncreaseInitiated = null;
        BossSpawnInitiated = null;
        BossModuleKillInitiated = null;
        BossExplosionInitiated = null;
        EnemyInChanceStageKillInitiated = null;
        ChanceBonusCountStartInitiated = null;
        ChanceBonusPointsToScoreAddingInitiated = null;
        StarParticlesToggleInitiated = null;
        SoundEffectPlayInitiated = null;
        SoundBGMPlayInitiated = null;
        SoundBGMStopInitiated = null;
        CurrentSoundBGMStopInitiated = null;
        IsBGMPlayingInitiated = null;
        CurrentPlayingBGMSilencingInitiated = null;
        AllEnemiesDeleteInitiated = null;
        ShootingModeToggleInitiated = null;
        RocketAddInitialized = null;
        BonusSpawnInitiated = null;
        GetPlayerShipStartingPositionInitiated = null;
        WeaponBonusKillInitiated = null;
        CurrentBGMToggleInitiated = null;
    }
}
