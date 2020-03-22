using UnityEngine;

public class GyrussGameManager : MonoBehaviour
{
    [SerializeField] private GyrussEventManager gyrussEventManager;
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private StageManager stageManager;

    private GyrussGameManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    

    public GyrussGameManager Instance => instance;

    public GyrussEventManager GyrussEventManager => gyrussEventManager;

    public PlayerInputManager PlayerInputManager => playerInputManager;

    public StageManager StageManager => stageManager;
    
    public LevelManager LevelManager => levelManager;
}
