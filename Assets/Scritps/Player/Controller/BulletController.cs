using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    [SerializeField] private Vector3 bulletScale = new Vector3(0.03f, 0.03f, 0);
    [SerializeField] private Rigidbody2D bulletRB;

    private Transform bulletTransform;
    private bool leftBullet;

    private float scalingToCenter = 0.005f;
    
    private void Start()
    {
        bulletTransform = transform;
        // bulletRB.velocity = bulletTransform.up * speed;
    }

    void Update()
    {
        if (transform.localScale.x > 0)
            transform.localScale -= bulletScale;
        
        // if (bulletTransform.position.y < 0) {
        //     if (leftBullet) 
        //         bulletTransform.localPosition += new Vector3( scalingToCenter, 0, 0);
        //     else if (!leftBullet) 
        //         bulletTransform.localPosition -= new Vector3( scalingToCenter, 0, 0);
        // }
        // else {
        //     if (leftBullet) 
        //         bulletTransform.localPosition -= new Vector3( scalingToCenter, 0, 0);
        //     else if (!leftBullet) 
        //         bulletTransform.localPosition += new Vector3( scalingToCenter, 0, 0);
        // }
        
        bulletTransform.localPosition += bulletTransform.up * (speed * 0.05f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MapCenter")) {
            Destroy(transform.gameObject);
        }
    }

    public bool LeftBullet
    {
        get => leftBullet;
        set => leftBullet = value;
    }
}
