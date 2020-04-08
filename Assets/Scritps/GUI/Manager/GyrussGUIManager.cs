using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
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
    }
    
    private void SetScoreText(int score)
    {
        string scoreString = score.ToString();
        int zeros = 7;
        int scoreLen = scoreString.Length;
        int neededZeros = zeros - scoreLen;

        for (int i = 0; i < neededZeros; i++) { scoreString = scoreString.Insert(0, "0"); }

        scoreText.text = scoreString;
        
        //TODO: if score > hiScore set also hiScore
    }

    private void SetStagesText(int stages)
    {
        stageText.text = stages.ToString();
    }

    private void IncreaseLives()
    {
        int currentLives = livesTransform.childCount;
        
        if (currentLives > 7) return;
        Instantiate(lifeIcon, livesTransform, true);
    }

    private void DecreaseLives(int lives)
    {
        if (lives > 7) return;
        Destroy(livesTransform.GetChild(0));
    }

    private void IncreaseRockets()
    {
        int currentRockets = rocketsTransform.childCount;
        
        if (currentRockets > 8) return;
        Instantiate(rocketIcon, rocketsTransform, true);
    }

    private void DecreaseRockets(int rockets)
    {
        if (rockets > 8) return;
        Destroy(rocketsTransform.GetChild(0));
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
    
    private void OnDestroy()
    {
        GyrussGUIEventManager.ClearDelegates();
    }
}
