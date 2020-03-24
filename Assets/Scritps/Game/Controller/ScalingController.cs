using UnityEngine;

public class ScalingController : MonoBehaviour
{
    private float scalingFactor;
    private float distanceToCenter;
    private float currentDistanceToCenter;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerPos = GyrussGameManager.Instance.PlayerInputManager.GetPlayerPosition();
        Vector3 centerPos = Vector3.zero;

        distanceToCenter = Vector3.Distance(playerPos, centerPos);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPosition = transform.position;
        currentDistanceToCenter = Vector3.Distance(currPosition, Vector3.zero);

        scalingFactor = currentDistanceToCenter / distanceToCenter;
        
        transform.localScale = new Vector3(scalingFactor, scalingFactor, 0);
    }
}
