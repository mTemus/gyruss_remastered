using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    void Update()
    {
        if (transform.gameObject.activeSelf)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * speed);
            
            if (transform.position == Vector3.zero)
                Destroy(transform.gameObject);
        }
    }

}
