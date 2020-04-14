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

    [Header("Icons")] 
    [SerializeField] private GameObject lifeIcon;
    [SerializeField] private GameObject rocketIcon;

    [Header("Groups")] 
    [SerializeField] private Transform rocketsTransform;
    [SerializeField] private Transform livesTransform;

    [Header("Prefabs")] 
    [SerializeField] private GameObject bonusTextPrefab;
    
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
        GyrussGUIEventManager.GUIToggleInitiated += ToggleGUI;
        GyrussGUIEventManager.WarpsTextSetupInitiated += SetWarpsText;
        GyrussGUIEventManager.WarpsTextToggleInitiated += ToggleWarpsText;
        GyrussGUIEventManager.ReadyTextToggleInitiated += ToggleReadyText;
        GyrussGUIEventManager.ScoreTextToggleInitiated += ToggleScoreText;
        GyrussGUIEventManager.LifeIconsInitializeInitiated += InitializeLifeIcons;
        GyrussGUIEventManager.RocketIconsInitializeInitiated += InitializeRocketIcons;
        GyrussGUIEventManager.HiScoreTextSetupInitiated += SetHiScoreText;
        GyrussGUIEventManager.WaveBonusTextShowInitiated += ShowWaveBonusText;
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
        
        if (currentRockets > 8) return;
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
    
    private void ToggleGUI()
    {
        GUI.SetActive(!GUI.activeSelf);
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
    
    
    private void OnDestroy()
    {
        GyrussGUIEventManager.ClearDelegates();
    }
}
