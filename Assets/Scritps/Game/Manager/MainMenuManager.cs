using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main menu parts")] 
    [SerializeField] private GameObject startGameText = null;
    [SerializeField] private GameObject exitGameText = null;
    [SerializeField] private Text hiScoreText = null;
    [SerializeField] private GameObject playerShip = null;

    private FixedButton startGameButton;
    private FixedButton exitGameButton;
    
    void Start()
    {
        startGameButton = startGameText.GetComponent<FixedButton>();
        exitGameButton = exitGameText.GetComponent<FixedButton>();
        
        int hiScore = PlayerPrefs.GetInt("hiScore");
        SetHiScoreText(hiScore);
    }

    void Update()
    {
        if (startGameButton.Pressed) {
            MovePlayerShipOnClick(startGameText);
            SceneManager.LoadScene("GameScene");
        }

        if (exitGameButton.Pressed) {
            MovePlayerShipOnClick(exitGameText);
            Application.Quit();
        }
    }

    private void MovePlayerShipOnClick(GameObject text)
    {
        Vector3 pos = playerShip.transform.position;
        pos.y = text.transform.position.y;
        playerShip.transform.position = pos;
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

    public void ExitApplication()
    {
        Application.Quit();
    }
}
