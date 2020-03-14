using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector3 bulletScale = new Vector3(0.03f, 0.03f, 0);

    private Transform bulletTransform;
    
    
    private void Start()
    {
        bulletTransform = transform;
    }

    void Update()
    {
        if (transform.localScale.x > 0)
            transform.localScale -= bulletScale;
        
        if (bulletTransform.localScale.x < 0) {
            Destroy(transform.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("MapCenter")) {
        //     Destroy(transform.gameObject);
        // }
    }
}
