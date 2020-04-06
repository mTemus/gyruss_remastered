using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private Transform[] minimapPoints;
    [Header("Player")]
    [SerializeField] private GameObject playerShipIcon;
    [SerializeField] private Animator playerShipAnimator;

    private float speed = 1f;
    private int currentPointIndex = 0;
    private Vector3 currentLevelPosition;

    private bool move;
    private static readonly int Speed = Animator.StringToHash("speed");

    private void Start()
    {
        SetDelegates();
    }

    private void Update()
    {
        if (move) {
            if (!(playerShipIcon.transform.position == currentLevelPosition)) {
                playerShipIcon.transform.position =
                    Vector3.MoveTowards(playerShipIcon.transform.position, currentLevelPosition, speed * Time.deltaTime);
                playerShipAnimator.SetFloat(Speed, speed);
            }
            else {
                playerShipAnimator.SetFloat(Speed, 0);
                
                GyrussGameManager.Instance.SetConditionInTimer("minimapArrivalAtPlanet", true);
                move = false;
            }
        }
    }

    private void SetDelegates()
    {
        GyrussEventManager.MoveToLevelOnMinimapInitiated += MoveToLevel;
    }

    private void MoveToLevel(int levelIndex)
    {
        currentLevelPosition = minimapPoints[levelIndex].position;
        move = true;
    }

}
