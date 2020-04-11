using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    private Vector3 playerPosition;
    private Vector3 exitPosition;

    private bool move;

    private void Update()
    {
        if (!move) return;
        
        transform.position = Vector3.MoveTowards(transform.position, exitPosition, speed * Time.deltaTime);

        if (transform.position == exitPosition) Destroy(transform.gameObject); 
    }
    
    public void CalculateExitPosition()
    {
        float newX = 3;

        float x1;
        float x2;
        
        if (playerPosition.x < 0) {
            if (playerPosition.y < 0) {
                x1 = -newX;
                x2 = -newX;
            }
            else {
                x1 = -newX;
                x2 = newX;
            }
        }
        else {
            if (playerPosition.y < 0) {
                x1 = newX;
                x2 = -newX;
            }
            else {
                x1 = newX;
                x2 = newX;
            }
        }
        
        exitPosition = new Vector3(playerPosition.x + x1, playerPosition.y + x2, 0);
        move = true;
    }
    
    public Vector3 PlayerPosition
    {
        get => playerPosition;
        set => playerPosition = value;
    }
}
