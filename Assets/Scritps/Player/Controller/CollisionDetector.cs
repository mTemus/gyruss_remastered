using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag) {
            case "Asteroid":
                GyrussGameManager.Instance.KillPlayer();
                break;
            
            case "Enemy":
                if (other.GetComponent<EnemyController>().CurrentEnemyState == EnemyStates.attack) {
                    GyrussGameManager.Instance.KillPlayer();
                }
                break;
            
            case "EnemyBullet":
                GyrussGameManager.Instance.KillPlayer();
                break;
        }
    }

    private void OnEnable()
    {
        GyrussGameManager.Instance.ToggleReadyText();
        GyrussGameManager.Instance.TogglePlayerSpawned();
        GyrussGameManager.Instance.PlaySoundEffect("player-spawn");
    }

    private void OnDisable()
    {
        GyrussGameManager.Instance.TogglePlayerSpawned();
        GyrussGameManager.Instance.PlaySoundEffect("player-death");
    }
}
