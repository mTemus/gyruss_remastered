using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Ship parts")]
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject shootingPointSingleGO;
    [SerializeField] private GameObject shootingPointDoubleGO;
    
    [Header("Other")] 
    [SerializeField] private GameObject bulletPrefab;
    
    [Header("Map")]
    [SerializeField] private SpriteRenderer background;

    [Header("Pools")] 
    [SerializeField] private Transform playerBulletPool;
    
    private float speed = 150f;
    private float reload;
    private bool doubleBulletMode = false;
    
    private Transform singleShootingPoint;
    private Transform doubleShootingPointOne;
    private Transform doubleShootingPointTwo;
    
    void Start()
    {
        Vector3 startingPosition = Vector3.zero;
        reload = 1;
        
        // Calculating starting position
        Bounds backgroundBounds = background.bounds;
        startingPosition = new Vector3(0, -((backgroundBounds.size.y / 4) - 0.2f), 0);
        playerShip.transform.position = startingPosition;

        singleShootingPoint = shootingPointSingleGO.transform;
        doubleShootingPointOne = shootingPointDoubleGO.transform.GetChild(0);
        doubleShootingPointTwo = shootingPointDoubleGO.transform.GetChild(1);
        
        ToggleShootingMode();
    }

    void Update()
    {
        // TODO: Change input for android to be like in original, when left, go only left, when top, only top etc.
        
        reload += Time.deltaTime;
        
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
        if (!(reload >= 0.4f)) return;
        reload = 0;

        if (!doubleBulletMode) {
            Instantiate(bulletPrefab, singleShootingPoint.position, singleShootingPoint.rotation, playerBulletPool);
        }
        else {
            Instantiate(bulletPrefab, doubleShootingPointOne.position, doubleShootingPointOne.rotation, playerBulletPool).GetComponent<BulletController>().LeftBullet = true;
            Instantiate(bulletPrefab, doubleShootingPointTwo.position, doubleShootingPointTwo.rotation, playerBulletPool).GetComponent<BulletController>().LeftBullet = false;
        }

        if (Input.GetKeyUp(KeyCode.Space)) { reload = 1; }

    }

    public void ToggleShootingMode()
    {
        doubleBulletMode = !doubleBulletMode;
        
        if (doubleBulletMode) {
            shootingPointSingleGO.SetActive(false);
            shootingPointDoubleGO.SetActive(true);
        } else {
            shootingPointSingleGO.SetActive(true);
            shootingPointDoubleGO.SetActive(false);
        }
        
    }
    
}
