using UnityEngine;

public class ReviveParticleController : MonoBehaviour
{
    private Vector3 shipPosition;
    private float speed = 3f;
    private bool move;

    private void Update()
    {
        if (!move) return;
        
        if (transform.position != shipPosition) {
            MoveToShipPosition();
        }
        else {
            move = false;
            GyrussGameManager.Instance.RegisterReviveParticle(transform.gameObject);
        }
    }

    private void MoveToShipPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, shipPosition, speed * Time.deltaTime);
    }

    public void PrepareMovement(Vector3 shipPosition)
    {
        this.shipPosition = shipPosition;
        move = true;
    }
}
