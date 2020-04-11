using UnityEngine;

public class PointsRotator : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.back, speed * Time.deltaTime);
    }
}
