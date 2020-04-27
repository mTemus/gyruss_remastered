using UnityEngine;

public class SingleEnemyController : MonoBehaviour
{
    [Header("Bonus properties")]
    [SerializeField] private string enemyType;

    private bool bonusAdded;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerBullet")) return;
        
        if (enemyType.Equals("weaponBonus")) {
            GyrussGameManager.Instance.ToggleShootingMode();
        } else if (enemyType.Equals("rocketBonus")) {
            if (bonusAdded) return; 
            GyrussGameManager.Instance.AddRocket();
            bonusAdded = true;
        }

        GyrussGameManager.Instance.PlaySoundEffect("enemyShipDeath-center");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Transform myTransform = transform;
        myTransform.parent.GetComponent<BonusEnemyController>().DecreaseEnemies();
        GyrussGameManager.Instance.CreateExplosion(myTransform.position, "miniBoss");
        GyrussGameManager.Instance.KillEnemy();
    }
}
