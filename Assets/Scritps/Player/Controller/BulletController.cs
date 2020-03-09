using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector3 bulletScale = new Vector3(0.02f, 0.02f, 0);

    private Transform bulletTransform;
    
    private void Start()
    {
        bulletTransform = transform;
    }

    void Update()
    {
        if (transform.localScale.x > 0)
            transform.localScale -= bulletScale;

        bulletTransform.localPosition += bulletTransform.up * (speed * 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapCenter")) {
            Destroy(transform.gameObject);
        }
    }
}
