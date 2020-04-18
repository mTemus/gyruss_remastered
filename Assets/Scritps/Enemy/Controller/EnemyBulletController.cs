using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed = 2f;

    private bool move;

    void Update()
    {
        if (!move) return;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition) {
            Destroy(transform.gameObject);
        }
    }
    
    public void StartBullet(Vector3 targetPos)
    {
        targetPosition = targetPos;
        move = true;
    }
}
