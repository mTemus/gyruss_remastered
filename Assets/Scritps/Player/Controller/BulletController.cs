using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    [SerializeField] private Vector3 bulletScale = new Vector3(0.03f, 0.03f, 0);
    [SerializeField] private Rigidbody2D bulletRB;

    private void Start()
    {
        bulletRB.velocity = transform.up * speed;
    }

    void Update()
    {
        transform.localScale -= bulletScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapCenter")) {
            Destroy(transform.gameObject);
        }
    }
}
