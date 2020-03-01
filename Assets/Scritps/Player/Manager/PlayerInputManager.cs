using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Ship parts")]
    [SerializeField] private GameObject playerShip;
    [SerializeField] private Transform shootingPoint;

    [Header("Other")] 
    [SerializeField] private GameObject bulletPrefab;
    
    [Header("Map")]
    [SerializeField] private SpriteRenderer background;

    private float speed = 150f;
    
    void Start()
    {
        Camera cam = Camera.main;
        Vector3 startingPosition = Vector3.zero;
        
        // if (cam.pixelWidth < cam.pixelHeight) {
            // Vector3 p1 = cam.WorldToScreenPoint(Vector3.zero);
            // Vector3 p2 = cam.WorldToScreenPoint(Vector3.right);
            // float dist = Vector3.Distance(p1, p2);
            // float halfOfWidth = (cam.pixelWidth * (1 / dist)) - 0.5f;
            
            // startingPosition = new Vector3(0, -(halfOfWidth / 2), 0);
        // }
        // else if (cam.pixelWidth > cam.pixelHeight) {
        //     Vector3 p1 = cam.WorldToScreenPoint(Vector3.zero);
        //     Vector3 p2 = cam.WorldToScreenPoint(Vector3.up);
        //     float dist = Vector3.Distance(p1, p2);
        //     float halfOfHeight = (cam.pixelHeight * (1 / dist)) - 0.5f;
        //     
        //     startingPosition = new Vector3(0, -(halfOfHeight / 2), 0);
        // }

        Bounds backgroundBounds = background.bounds;
        startingPosition = new Vector3(0, -((backgroundBounds.size.y / 4) - 0.2f), 0);
        playerShip.transform.position = startingPosition;


    }

    void Update()
    {
        // TODO: Change input for android to be like in original, when left, go only left, when top, only top etc.
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateShip(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateShip(Vector3.back);
        }

        if (Input.GetKey(KeyCode.Space)) {
            ShootBullet();
        }
    }

    private void RotateShip(Vector3 rotateAxis)
    {
        playerShip.transform.RotateAround(Vector3.zero,rotateAxis, Time.deltaTime * speed);
    }

    private void ShootBullet()
    {
        Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
    }
    
    
}
