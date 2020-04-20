using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
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
        exitPosition = new Vector3(playerPosition.x * 2, playerPosition.y * 2, 0);
        move = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet")) {
            GyrussGameManager.Instance.PlaySoundEffect("asteroid-notHurt");
            Destroy(other.transform.gameObject);
        }
        else if (other.CompareTag("Rocket")) {
            GyrussGameManager.Instance.PlaySoundEffect("module-explosion");
            GyrussGameManager.Instance.CreateExplosion(transform.position, "normal");
            Destroy(other.transform.gameObject);
            Destroy(transform.gameObject);
        }
    }

    public Vector3 PlayerPosition
    {
        get => playerPosition;
        set => playerPosition = value;
    }
}
