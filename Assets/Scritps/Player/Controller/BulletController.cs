using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private Rigidbody2D bulletRB;

    private void Start()
    {
        bulletRB.velocity = transform.up * (speed * Time.deltaTime);
    }

    void Update()
    {
        transform.localScale -= new Vector3(0.05f, 0.05f, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapCenter")) {
            Destroy(transform.gameObject);
        }
    }
}
