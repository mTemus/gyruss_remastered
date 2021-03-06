using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GyrussGUIManager : MonoBehaviour
{
    [Header("Main GUI")]
    [SerializeField] private GameObject GUI = null;
    [SerializeField] private GameObject exitGamePanel = null;
    
    [Header("Text fields")] 
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text hiScoreText = null;
    [SerializeField] private Text stageText = null;
    [SerializeField] private Text warpsLeftText = null;
    [SerializeField] private Text readyText = null;
    [SerializeField] private Text chanceBonusText = null;
    [SerializeField] private Text gameRestartText = null;
    [SerializeField] private Text pausedText = null;

    [Header("Icons")] 
    [SerializeField] private GameObject lifeIcon = null;
    [SerializeField] private GameObject rocketIcon = null;

    [Header("Groups")] 
    [SerializeField] private Transform rocketsTransform = null;
    [SerializeField] private Transform livesTransform = null;

    [Header("Prefabs")] 
    [SerializeField] private GameObject bonusTextPrefab = null;
    [SerializeField] private GameObject[] planetsTargets = null;
    
    private float currentVisibility = 0.25f;

    private int chanceTextBlinks;
    private int chanceBonusTextBlinks;
    private int restartCount = 10;
    
    private GameObject currentPlanet;
    
    private void Awake()
    {
        SetDelegates();
    }
    
    private void SetDelegates()
    {
        GyrussGUIEventManager.ScoreTextSetupInitiated += SetScoreText;
        GyrussGUIEventManager.StagesTextSetupInitiated += SetStagesText;
        GyrussGUIEventManager.LivesIconsIncreaseInitiated += IncreaseLives;
        GyrussGUIEventManager.LivesIconsDecreaseInitiated += DecreaseLives;
        GyrussGUIEventManager.RocketsIconsIncreaseInitiated += IncreaseRockets;
        GyrussGUIEventManager.RocketsIconsDecreaseInitiated += DecreaseRockets;
        GyrussGUIEventManager.WarpsTextSetupInitiated += SetWarpsText;
        GyrussGUIEventManager.WarpsTextToggleInitiated += ToggleWarpsText;
        GyrussGUIEventManager.ReadyTextToggleInitiated += ToggleReadyText;
        GyrussGUIEventManager.ScoreTextToggleInitiated += ToggleScoreText;
        GyrussGUIEventManager.LifeIconsInitializeInitiated += InitializeLifeIcons;
        GyrussGUIEventManager.RocketIconsInitializeInitiated += InitializeRocketIcons;
        GyrussGUIEventManager.HiScoreTextSetupInitiated += SetHiScoreText;
        GyrussGUIEventManager.WaveBonusTextShowInitiated += ShowWaveBonusText;
        GyrussGUIEventManager.GUIVisibilityIncreaseInitiated += IncreaseGUIVisibility;
        GyrussGUIEventManager.GUIVisibilityDecreaseInitiated += DecreaseGUIVisibility;
        GyrussGUIEventManager.PlanetDestroyInitiated += DestroyDisplayedPlanet;
        GyrussGUIEventManager.PlanetDisplayInitiated += DisplayPlanet;
        GyrussGUIEventManager.ChanceTextBlinkInitiated += BlinkChanceStageText;
        GyrussGUIEventManager.ChanceStageTextDisplayInitiated += DisplayChanceStageText;
        GyrussGUIEventManager.ChanceBonusScoreDisplayInitiated += SetChanceBonusText;
        GyrussGUIEventManager.ChanceBonusTextBlinkInitiated += BlinkChanceBonusText;
        GyrussGUIEventManager.ToggleChanceBonusTryInitiated += ToggleChanceBonusText;
        GyrussGUIEventManager.GameOverTextDisplayInitiated += DisplayGameOverText;
        GyrussGUIEventManager.GameRestartCountInitiated += CountToRestart;
        GyrussGUIEventManager.GameEndingDisplayInitiated += DisplayGameEnding;
        GyrussGUIEventManager.PausedTextToggleInitiated += TogglePausedText;
        GyrussGUIEventManager.ExitPanelToggleInitiated += ToggleExitPanel;
    }
    
    private void SetScoreText(int score)
    {
        string scoreString = score.ToString();
        int zeros = 7;
        int scoreLen = scoreString.Length;
        int neededZeros = zeros - scoreLen;

        for (int i = 0; i < neededZeros; i++) { scoreString = scoreString.Insert(0, "0"); }

        scoreText.text = scoreString;
    }

    private void SetHiScoreText(int hiScore)
    {
        string hiScoreString = hiScore.ToString();
        int zeros = 7;
        int hiScoreLen = hiScoreString.Length;
        int neededZeros = zeros - hiScoreLen;

        for (int i = 0; i < neededZeros; i++) { hiScoreString = hiScoreString.Insert(0, "0"); }
        hiScoreText.text = hiScoreString;
    }

    private void SetStagesText(int stages)
    {
        stageText.text = stages.ToString();
    }

    private void IncreaseLives()
    {
        int currentLives = livesTransform.childCount;
        
        if (currentLives > 7) return;
        GameObject lifeGO = Instantiate(lifeIcon, livesTransform, true);
        lifeGO.transform.localScale = new Vector3(1.5f, 1.5f,1);
    }

    private void DecreaseLives(int lives)
    {
        if (lives > 7) return;
        Destroy(livesTransform.GetChild(0).gameObject);
    }

    private void IncreaseRockets()
    {
        int currentRockets = rocketsTransform.childCount;
        
        if (currentRockets > 7) return;
        GameObject rocketGO = Instantiate(rocketIcon, rocketsTransform, true);
        rocketGO.transform.localScale = new Vector3(1.5f, 1.5f, 1);
    }

    private void DecreaseRockets(int rockets)
    {
        if (rockets > 8) return;
        Destroy(rocketsTransform.GetChild(0).gameObject);
    }

    private void SetWarpsText(int warps, string planet)
    {
        warpsLeftText.text = warps + " WARPS TO " + planet.ToUpper(CultureInfo.CurrentCulture);
    }

    private void DisplayChanceStageText()
    {
        warpsLeftText.text = "CHANCE STAGE";
        chanceTextBlinks = 0;
        ToggleWarpsText();
        BlinkChanceStageText();
    }

    private void DisplayGameOverText()
    {
        rocketsTransform.gameObject.SetActive(false);
        livesTransform.gameObject.SetActive(false);
        
        warpsLeftText.text = "GAME OVER";
        gameRestartText.enabled = true;
        ToggleWarpsText();
        CountToRestart();
    }

    private void BlinkChanceStageText()
    {
        switch (chanceTextBlinks) {
            case 4:
                GyrussGameManager.Instance.SetConditionInTimer("readyTextDelay", true);
                break;
            case 30:
                chanceTextBlinks = 0;
                return;
        }
        
        warpsLeftText.color = chanceTextBlinks % 2 == 0 ? Color.white : new Color(252/255f, 164/255f, 75/255f, 1);
        chanceTextBlinks++;

        GyrussGameManager.Instance.SetConditionInTimer("chanceBlink", true);
    }
    

    private void ToggleWarpsText()
    {
        warpsLeftText.enabled = !warpsLeftText.enabled;
    }

    private void ToggleReadyText()
    {
        readyText.enabled = !readyText.enabled;
    }

    private void ToggleScoreText()
    {
        scoreText.enabled = !scoreText.enabled;
        hiScoreText.enabled = !hiScoreText.enabled;
    }

    private void InitializeLifeIcons()
    {
        int playerLives = GyrussGameManager.Instance.GetPlayerLives();
        int initializedLives = livesTransform.childCount;

        if (initializedLives >= playerLives) return;
        
        IncreaseLives();
        GyrussGameManager.Instance.PlaySoundEffect("lifeIcon-initialize");
        GyrussGameManager.Instance.SetConditionInTimer("lifeIconsInitialization", true);
    }
    
    private void InitializeRocketIcons()
    {
        int playerRockets = GyrussGameManager.Instance.GetPlayerRockets();
        int initializedRockets = rocketsTransform.childCount;

        if (initializedRockets >= playerRockets) return;
        
        IncreaseRockets();
        GyrussGameManager.Instance.PlaySoundEffect("rocketIcon-initialize");
        GyrussGameManager.Instance.SetConditionInTimer("rocketIconsInitialization", true);
    }

    private void ShowWaveBonusText(int score)
    {
        Vector3 playerPos = GyrussGameManager.Instance.GetPlayerShipPosition();
        Vector3 textOffset = new Vector3();
        
        if (playerPos.x > 0) { textOffset.x = -0.8f; }
        else { textOffset.x = 0.8f; }

        int yParity = Random.Range(-1, 1);
        float yValue = Random.Range(-0.6f, 0.6f);

        textOffset.y = yValue * yParity;
        
        GameObject bonusText = Instantiate(bonusTextPrefab, GUI.transform, true);
        bonusText.GetComponent<Text>().text = score.ToString();
        bonusText.transform.position = playerPos + textOffset;
        bonusText.transform.localScale = new Vector3(1,1,0);
    }

    private void ToggleChanceBonusText()
    {
        chanceBonusText.enabled = !chanceBonusText.enabled;
    }

    private void SetChanceBonusText(int killedShips)
    {
        if (killedShips < 40) {
            string killedShipsString = killedShips.ToString();
            if (killedShips < 10) { killedShipsString = killedShipsString.Insert(0, "0"); }

            string bonusForKilledShips = (killedShips * 100).ToString();

            chanceBonusText.text = "BONUS\n" + killedShipsString + " x 100     " + bonusForKilledShips + " PTS";
        }
        else {
            chanceBonusText.text = "CONGRATULATIONS!\n PERFECT     20000 PTS";
        }
        ToggleChanceBonusText();
        BlinkChanceBonusText();
    }
    
    private void BlinkChanceBonusText()
    {
        switch (chanceBonusTextBlinks) {
            case 10:
                GyrussGameManager.Instance.AddChanceBonusPointsToScore();
                break;
            case 40:
                chanceBonusTextBlinks = 0;
                return;
        }
        
        chanceBonusText.color = chanceBonusTextBlinks % 2 == 0 ? Color.white : new Color(252/255f, 164/255f, 75/255f, 1);
        chanceBonusTextBlinks++;

        GyrussGameManager.Instance.SetConditionInTimer("chanceBonusBlink", true);
    }

    private void DecreaseGUIVisibility()
    {
        CanvasGroup GUICG = GUI.GetComponent<CanvasGroup>();
        
        GUICG.alpha = 1 - currentVisibility;
        currentVisibility += 0.25f;

        if (GUICG.alpha != 0) {
            GyrussGameManager.Instance.SetConditionInTimer("GUIVisibilityDecrease", true);
        }
        else {
            currentVisibility = 0.25f;
        }
    }

    private void IncreaseGUIVisibility()
    {
        CanvasGroup GUICG = GUI.GetComponent<CanvasGroup>();
        GUICG.alpha = currentVisibility;
        currentVisibility += 0.25f;

        if (GUICG.alpha == 1) {
            currentVisibility = 0.25f;
            GyrussGameManager.Instance.SetConditionInTimer("nextStageDelay", true);
        }
        else {
            GyrussGameManager.Instance.SetConditionInTimer("GUIVisibilityIncrease", true);
        }
    }

    private void DisplayPlanet(int planetId)
    {
        currentPlanet = Instantiate(planetsTargets[planetId]);
        currentPlanet.transform.position = Vector3.zero;
    }

    private void DestroyDisplayedPlanet()
    {
        Destroy(currentPlanet);
    }

    private void DisplayGameEnding()
    {
        warpsLeftText.fontSize = 12;
        warpsLeftText.text = "KONIEC DEMA STWORZONEGO NA PROJEKT Z PROGRAMOWANIA APLIKACJI MOBILNYCH\n" +
                             "MARCIN WOJCIK & MATEUSZ MAJEWSKI \n" +
                             "GRUPA 4, INFORMATYKA STOSOWANA SEMESTR 6";
        ToggleWarpsText();
        
        gameRestartText.enabled = true;
        CountToRestart();
    }

    private void CountToRestart()
    {
        gameRestartText.text = "GAME WILL RESTART IN: " + restartCount--;

        if (restartCount >= 0) {
            GyrussGameManager.Instance.SetConditionInTimer("gameRestart", true);
        }
        else {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    private void TogglePausedText()
    {
        pausedText.enabled = !pausedText.enabled;
    }

    private void ToggleExitPanel()
    {
        exitGamePanel.SetActive(!exitGamePanel.activeSelf);
    }
    
    private void OnDestroy()
    {
        GyrussGUIEventManager.ClearDelegates();
    }
}
