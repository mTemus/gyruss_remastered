using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Ship parts")]
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject shootingPointSingleGO;
    [SerializeField] private GameObject shootingPointDoubleGO;
    
    [Header("Other")] 
    [SerializeField] private GameObject bulletPrefabSingle;
    [SerializeField] private GameObject bulletPrefabDouble;
    
    [Header("Map")]
    [SerializeField] private SpriteRenderer background;

    [Header("Pools")] 
    [SerializeField] private Transform playerBulletPool;
    
    private float speed = 150f;
    private float reload = 1;
    private bool doubleBulletMode = false;
    
    private Transform shootingPointSingle;
    private Transform shootingPointDoubleOne;
    private Transform shootingPointDoubleTwo;
    
    void Start()
    {
        // Calculating starting position
        Bounds backgroundBounds = background.bounds;
        Vector3 startingPosition = new Vector3(0, -((backgroundBounds.size.y / 4) - 0.2f), 0);
        playerShip.transform.position = startingPosition;

        shootingPointSingle = shootingPointSingleGO.transform;
        shootingPointDoubleOne = shootingPointDoubleGO.transform.GetChild(0);
        shootingPointDoubleTwo = shootingPointDoubleGO.transform.GetChild(1);
        
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
        if (!(reload >= 0.3f)) return;
        reload = 0;

        if (!doubleBulletMode) 
            Instantiate(bulletPrefabSingle, shootingPointSingle.position, playerShip.transform.rotation, playerBulletPool);
        else 
            Instantiate(bulletPrefabDouble, shootingPointSingle.position, playerShip.transform.rotation, playerBulletPool);
        

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

    public Vector3 GetPlayerPosition()
    {
        return playerShip.transform.position;
    }
}
