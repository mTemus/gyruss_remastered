using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GyrussGUIManager : MonoBehaviour
{
    [Header("Main GUI")]
    [SerializeField] private GameObject GUI;
    
    [Header("Text fields")] 
    [SerializeField] private Text scoreText;
    [SerializeField] private Text hiScoreText;
    [SerializeField] private Text stageText;
    [SerializeField] private Text warpsLeftText;
    [SerializeField] private Text readyText;
    [SerializeField] private Text chanceBonusText;

    [Header("Icons")] 
    [SerializeField] private GameObject lifeIcon;
    [SerializeField] private GameObject rocketIcon;

    [Header("Groups")] 
    [SerializeField] private Transform rocketsTransform;
    [SerializeField] private Transform livesTransform;

    [Header("Prefabs")] 
    [SerializeField] private GameObject bonusTextPrefab;
    [SerializeField] private GameObject[] planetsTargets;
    
    private float currentVisibility = 0.25f;

    private int chanceTextBlinks;
    private int chanceBonusTextBlinks;
    
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

    private void BlinkChanceStageText()
    {
        switch (chanceTextBlinks) {
            case 8:
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
        GyrussGameManager.Instance.SetConditionInTimer("lifeIconsInitialization", true);
    }
    
    private void InitializeRocketIcons()
    {
        int playerRockets = GyrussGameManager.Instance.GetPlayerRockets();
        int initializedRockets = rocketsTransform.childCount;

        if (initializedRockets >= playerRockets) return;
        
        IncreaseRockets();
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
        string killedShipsString = killedShips.ToString();
        if (killedShips < 10) { killedShipsString = killedShipsString.Insert(0, "0"); }

        string bonusForKilledShips = (killedShips * 100).ToString();

        chanceBonusText.text = "BONUS\n" + killedShipsString + " x 100     " + bonusForKilledShips + " PTS";
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
    
    private void OnDestroy()
    {
        GyrussGUIEventManager.ClearDelegates();
    }
}
