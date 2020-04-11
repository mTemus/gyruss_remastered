using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private float speed = 0.8f;
    [SerializeField] private float deathPeriod = 0.6f;

    private float deathCounter;
    private bool die;
    
    private Vector3 deathPosition;

    void Update()
    {
        if (!die) return;
        
        if (deathCounter == 0) {
            SetDeathRoute();
        } else if (deathCounter < deathPeriod) {
            MoveOnDeathRoute();
        }
        else if (deathCounter >= deathPeriod) {
            Destroy(transform.gameObject);
        }

        deathCounter += Time.deltaTime;
    }

    private void SetDeathRoute()
    {
        Vector3 currentPosition = transform.position;
        float newX = Random.Range(0.1f, 0.7f);
        float newY = Random.Range(-0.2f, 0.2f);

        float x1;
        float x2;
        
        if (currentPosition.x < 0) {
            if (currentPosition.y < 0) {
                x1 = -newX;
                x2 = -newX + newY;
            }
            else {
                x1 = -newX;
                x2 = newX + newY;
            }
        }
        else {
            if (currentPosition.y < 0) {
                x1 = newX;
                x2 = -newX + newY;
            }
            else {
                x1 = newX;
                x2 = newX + newY;
            }
        }
        
        deathPosition = new Vector3(currentPosition.x + x1, currentPosition.y + x2, 0);
    }

    private void MoveOnDeathRoute()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, deathPosition, speed * Time.deltaTime);
    }
    
    public void Die()
    {
        die = true;
    }
}
