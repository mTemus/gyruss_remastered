using UnityEngine;

public class PointsRotator : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    
    void Update()
    {
        Transform pointTransform;
        (pointTransform = transform).RotateAround(Vector3.zero, Vector3.back, speed * Time.deltaTime);
        pointTransform.rotation = Quaternion.identity;
    }
}
