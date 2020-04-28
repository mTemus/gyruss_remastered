using UnityEngine;

public class BonusEnemyController : MonoBehaviour
{
    [Header("Bonus properties")]
    [SerializeField] private string bonusType = null;
    [SerializeField] private float speed = 0;

    private float rotationAngle;
    private float rotationDirection;
    private float blinkPeriod = 0.2f;
    private float blinkTime;
    
    private int blinkAmount;
    private int enemiesLeft = 3;
    
    private Vector3 originPoint = Vector3.zero;

    void Start()
    {
        blinkTime = blinkPeriod;

        Vector3 referencePos = GyrussGameManager.Instance.GetPlayerStartingPosition();
        referencePos.y /= 2f;

        float angle = Random.Range(-180f, 180f);

        foreach (Transform child in transform) {
            child.position = referencePos;
            child.RotateAround(originPoint, Vector3.forward, angle);
            angle += 20f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (blinkAmount < 10 ) {
            if (blinkTime <= 0) {
                foreach (Transform child in transform) {
                    SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                    sr.enabled = !sr.enabled;
                }
                
                blinkTime = blinkPeriod;
                blinkAmount++;
            }
            
            blinkTime -= Time.deltaTime;
            
            if (blinkAmount != 10) return;
            foreach (Transform child in transform) {
                child.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
        else {
            // move bonus
            transform.RotateAround(originPoint, Vector3.forward, speed * Time.deltaTime);
            rotationAngle += Time.deltaTime;
            rotationDirection += Time.deltaTime;

            // change rotation direction
            if (rotationDirection >= 5) {
                speed = -speed;
                rotationDirection = 0;
            }
            
            // move away from stage
            if (rotationAngle >= 20) {
                Vector3 currPos = transform.position;

                if (currPos.x >= 0) { originPoint.x += 0.1f; }
                else { originPoint.x -= 0.1f; }

                if (currPos.y >= 0) { originPoint.y += 0.1f; }
                else { originPoint.y -= 0.1f; }
            }

            // destroy object
            if (!(originPoint.y >= 5)) return;
            if (bonusType.Equals("weapon")) {
                float randomRespawn = Random.Range(15, 25);
                GyrussGameManager.Instance.SetPeriodInTimer("weaponBonusSpawn", randomRespawn);
                GyrussGameManager.Instance.SetConditionInTimer("weaponBonusSpawn", true);
            }
                
            Destroy(gameObject);
        }
    }

    public void DecreaseEnemies()
    {
        enemiesLeft--;

        if (enemiesLeft == 0) {
            Destroy(gameObject);
        }
    }
}
