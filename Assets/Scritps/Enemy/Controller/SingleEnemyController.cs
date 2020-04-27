using UnityEngine;

public class SingleEnemyController : MonoBehaviour
{
    [Header("Bonus properties")]
    [SerializeField] private string enemyType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerBullet")) return;
        
        if (enemyType.Equals("weaponBonus")) {
            GyrussGameManager.Instance.ToggleShootingMode();
        } else if (enemyType.Equals("rocketBonus")) {
            GyrussGameManager.Instance.AddRocket();
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
