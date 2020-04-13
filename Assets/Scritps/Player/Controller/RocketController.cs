using UnityEngine;

public class RocketController : MonoBehaviour
{
    [Header("Rocket parts")]
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject tail_1;
    [SerializeField] private GameObject tail_2;
    [SerializeField] private GameObject tail_3;

    [Header("Other")]
    [SerializeField] private float speed = 4f;

    private Vector3 startPosition;
    
    private float distanceFromStart;


    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
        CheckRocketParts();

        if (transform.position == Vector3.zero) {
            Destroy(transform.gameObject);
        }
        
    }
    
    private void CheckRocketParts()
    {
        Vector3 currentPosition = transform.position;
        distanceFromStart = Vector3.Distance(startPosition, currentPosition);
        
        if (!head.activeSelf) {
            if (distanceFromStart >= 0.05f) {
                head.SetActive(true);
            }
        } 
        else if (!tail_1.activeSelf) {
            if (distanceFromStart >= 0.1f) {
                tail_1.SetActive(true);
            }
        } 
        else if (!tail_2.activeSelf) {
            if (distanceFromStart >= 0.2f) {
                tail_2.SetActive(true);
            }
        } 
        else if (!tail_3.activeSelf) {
            if (distanceFromStart >= 0.3f) {
                tail_3.SetActive(true);
            }
        }
    }
}
