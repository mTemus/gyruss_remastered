using UnityEngine;

public class DeathParticleController : MonoBehaviour
{
    private Vector3 destinationPosition;

    private bool move;
    private float speed = 3f;
    
    
    void Update()
    {
        if (move) {
            transform.position = Vector3.MoveTowards(transform.position, destinationPosition, speed * Time.deltaTime);

            if (transform.position != destinationPosition) return;
            move = false;
            transform.gameObject.SetActive(false);
        }
    }

    public void SetDeathPosition(Vector3 position)
    {
        destinationPosition = position;
    }

    public void StartParticle()
    {
        move = true;
    }
    
}
