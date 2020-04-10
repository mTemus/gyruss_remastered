using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private float speed = 0.8f;
    [SerializeField] private float deathPeriod = 0.6f;

    private float deathCounter;
    private bool die;
    
    private Vector3 deathPosition;

    // Update is called once per frame
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
        float newX = Random.Range(0.5f, 5);
        float newY = Random.Range(0.5f, 5);

        if (currentPosition.x <= 0) { newX = -newX; }
        if (currentPosition.y <= 0) { newY = -newY; }

        
        // Debug.Log("-----------------");
        // Debug.Log(newX);
        // Debug.Log(newY);
        
        deathPosition = new Vector3(newX, newY, 0);
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
