using System;
using UnityEngine;

public class GyrussGUIEventManager : MonoBehaviour
{
    public static Action<int> ScoreTextSetupInitiated;
    public static Action<int> HiScoreTextSetupInitiated;
    public static Action<int> StagesTextSetupInitiated;
    public static Action<int> LivesIconsDecreaseInitiated;
    public static Action<int> RocketsIconsDecreaseInitiated;
    public static Action<int> WaveBonusTextShowInitiated;
    public static Action<int> PlanetDisplayInitiated;
    public static Action<int> ChanceBonusScoreDisplayInitiated;
    public static Action<int, string> WarpsTextSetupInitiated;
    public static Action LivesIconsIncreaseInitiated;
    public static Action RocketsIconsIncreaseInitiated;
    public static Action WarpsTextToggleInitiated;
    public static Action ReadyTextToggleInitiated;
    public static Action ScoreTextToggleInitiated;
    public static Action LifeIconsInitializeInitiated;
    public static Action RocketIconsInitializeInitiated;
    public static Action GUIVisibilityIncreaseInitiated;
    public static Action GUIVisibilityDecreaseInitiated;
    public static Action PlanetDestroyInitiated;
    public static Action ChanceTextBlinkInitiated;
    public static Action ChanceBonusTextBlinkInitiated;
    public static Action ChanceStageTextDisplayInitiated;
    public static Action ToggleChanceBonusTryInitiated;
    public static Action GameOverTextDisplayInitiated;
    public static Action GameRestartCountInitiated;
    public static Action GameEndingDisplayInitiated;
    public static Action PausedTextToggleInitiated;
    public static Action ExitPanelToggleInitiated;

    public static void OnExitPanelToggleInitiated()
    {
        ExitPanelToggleInitiated?.Invoke();
    }
    
    public static void OnPausedTextToggleInitiated()
    {
        PausedTextToggleInitiated?.Invoke();
    }
    
    public static void OnGameEndingDisplayInitiated()
    {
        GameEndingDisplayInitiated?.Invoke();
    }

    public static void OnGameRestartCountInitiated()
    {
        GameRestartCountInitiated?.Invoke();
    }
    
    public static void OnGameOverTextDisplayInitiated()
    {
        GameOverTextDisplayInitiated?.Invoke();
    }
    
    public static void OnToggleChanceBonusTryInitiated()
    {
        ToggleChanceBonusTryInitiated?.Invoke();
    }
    
    public static void OnChanceBonusTextBlinkInitiated()
    {
        ChanceBonusTextBlinkInitiated?.Invoke();
    }
    
    public static void OnChanceBonusScoreDisplayInitiated(int shipsKilled)
    {
        ChanceBonusScoreDisplayInitiated?.Invoke(shipsKilled);
    }
    
    public static void OnChanceStageTextDisplayInitiated()
    {
        ChanceStageTextDisplayInitiated?.Invoke();
    }
    
    public static void OnChanceTextBlinkInitiated()
    {
        ChanceTextBlinkInitiated?.Invoke();
    }
    
    public static void OnPlanetDestroyInitiated()
    {
        PlanetDestroyInitiated?.Invoke();
    }
    
    public static void OnPlanetDisplayInitiated(int planetId)
    {
        PlanetDisplayInitiated?.Invoke(planetId);
    }
    
    public static void OnGUIVisibilityDecreaseInitiated()
    {
        GUIVisibilityDecreaseInitiated?.Invoke();
    }
    
    public static void OnGUIVisibilityIncreaseInitiated()
    {
        GUIVisibilityIncreaseInitiated?.Invoke();
    }
    
    public static void OnWaveBonusTextShowInitiated(int score)
    {
        WaveBonusTextShowInitiated?.Invoke(score);
    }
    
    public static void OnRocketIconsInitializeInitiated()
    {
        RocketIconsInitializeInitiated?.Invoke();
    }
    
    public static void OnLifeIconsInitializeInitiated()
    {
        LifeIconsInitializeInitiated?.Invoke();
    }
    
    public static void OnScoreTextToggleInitiated()
    {
        ScoreTextToggleInitiated?.Invoke();
    }
    
    public static void OnReadyTextToggleInitiated()
    {
        ReadyTextToggleInitiated?.Invoke();
    }
    
    public static void OnWarpsTextToggleInitiated()
    {
        WarpsTextToggleInitiated?.Invoke();
    }
    
    public static void OnWarpsTextSetupInitiated(int warps, string planet)
    {
        WarpsTextSetupInitiated?.Invoke(warps, planet);
    }
    
    public static void OnRocketsIconsDecreaseInitiated(int rockets)
    {
        RocketsIconsDecreaseInitiated?.Invoke(rockets);
    }
    
    public static void OnRocketsIconsIncreaseInitiated()
    {
        RocketsIconsIncreaseInitiated?.Invoke();
    }
    
    public static void OnLivesIconsDecreaseInitiated(int lives)
    {
        LivesIconsDecreaseInitiated?.Invoke(lives);
    }
    
    public static void OnLivesIconsIncreaseInitiated()
    {
        LivesIconsIncreaseInitiated?.Invoke();
    }

    public static void OnStagesTextSetupInitiated(int stage)
    {
        StagesTextSetupInitiated?.Invoke(stage);
    }

    public static void OnHiScoreTextSetupInitiated(int hiScore)
    {
        HiScoreTextSetupInitiated?.Invoke(hiScore);
    }
    
    public static void OnScoreTextSetupInitiated(int score)
    {
        ScoreTextSetupInitiated?.Invoke(score);
    }

    public static void ClearDelegates()
    {
        ScoreTextSetupInitiated = null;
        StagesTextSetupInitiated = null;
        LivesIconsIncreaseInitiated = null;
        LivesIconsDecreaseInitiated = null;
        RocketsIconsIncreaseInitiated = null;
        RocketsIconsDecreaseInitiated = null;
        WarpsTextSetupInitiated = null;
        WarpsTextToggleInitiated = null;
        ReadyTextToggleInitiated = null;
        ScoreTextToggleInitiated = null;
        LifeIconsInitializeInitiated = null;
        RocketIconsInitializeInitiated = null;
        HiScoreTextSetupInitiated = null;
        WaveBonusTextShowInitiated = null;
        GUIVisibilityIncreaseInitiated = null;
        GUIVisibilityDecreaseInitiated = null;
        PlanetDisplayInitiated = null;
        PlanetDestroyInitiated = null;
        ChanceTextBlinkInitiated = null;
        ChanceStageTextDisplayInitiated = null;
        ChanceBonusScoreDisplayInitiated = null;
        ChanceBonusTextBlinkInitiated = null;
        ToggleChanceBonusTryInitiated = null;
        GameOverTextDisplayInitiated = null;
        GameRestartCountInitiated = null;
        GameEndingDisplayInitiated = null;
        PausedTextToggleInitiated = null;
        ExitPanelToggleInitiated = null;
    }
    
}
