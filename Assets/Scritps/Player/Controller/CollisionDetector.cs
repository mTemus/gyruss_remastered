using System;
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
                if (other.GetComponent<EnemyController>().currentEnemyState == EnemyStates.attack) {
                    
                }
                break;
            
            case "EnemyBullet":
                break;
        }
    }

    private void OnEnable()
    {
        GyrussGameManager.Instance.ToggleReadyText();
        GyrussGameManager.Instance.TogglePlayerSpawned();
    }

    private void OnDisable()
    {
        GyrussGameManager.Instance.TogglePlayerSpawned();
    }
}
