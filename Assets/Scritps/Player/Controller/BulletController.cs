using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private readonly Vector3 scaling = new Vector3(0.01f,0.01f,0);
    
    void Update()
    {
        if (transform.gameObject.activeSelf)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * speed);

            if (transform.localScale.x > 0)
                transform.localScale -= scaling;
            
            if (transform.position == Vector3.zero)
                Destroy(transform.gameObject);
        }
    }

}
