using System;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Ship parts")]
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject shootingPointSingleGO;
    [SerializeField] private GameObject shootingPointDoubleGO;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject warpingEffects;
    
    [Header("Prefabs")] 
    [SerializeField] private GameObject bulletPrefabSingle;
    [SerializeField] private GameObject bulletPrefabDouble;
    [SerializeField] private GameObject rocketPrefab;
    
    [Header("Map")]
    [SerializeField] private SpriteRenderer background;

    [Header("Pools")] 
    [SerializeField] private Transform playerBulletPool;
    
    private float speed = 150f;
    private float reload = 1;
    
    private bool doubleBulletMode = false;
    private bool spawned;
    private bool shootRocket;

    private int lives = 4;
    private int rockets = 1;
    private int currentScore = 0;

    private Vector3 playerStartingPosition;
    
    private Transform shootingPointSingle;
    private Transform shootingPointDoubleOne;
    private Transform shootingPointDoubleTwo;
    private static readonly int Entered = Animator.StringToHash("entered");
    private static readonly int Warping = Animator.StringToHash("warping");

    void Start()
    {
        SetDelegates();
        
        // Calculating starting position
        Bounds backgroundBounds = background.bounds;
        playerStartingPosition = new Vector3(0, -((backgroundBounds.size.y / 4) - 0.2f), 0);
        playerShip.transform.position = playerStartingPosition;
        
        // setting starting position in managers that use it
        GyrussGameManager.Instance.SetPlayerShipPosition(playerStartingPosition);
        
        // taking shooting points for shooting purposes
        shootingPointSingle = shootingPointSingleGO.transform;
        shootingPointDoubleOne = shootingPointDoubleGO.transform.GetChild(0);
        shootingPointDoubleTwo = shootingPointDoubleGO.transform.GetChild(1);
        
        // TODO: Remove this after weapon upgrade implementation
        ToggleShootingMode();
    }

    private void Update()
    {
        if (!spawned) return;

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

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            if (shootRocket) return;
            if (rockets <= 0) return;
            
            GyrussGameManager.Instance.SetRocketParticlesOnPosition();
        }
    }

    private void SetDelegates()
    {
        GyrussEventManager.GetPlayerShipPositionInitiated += GetPlayerPosition;
        GyrussEventManager.PlayerEnteredSetupInAnimatorInitiated += SetPlayerEnteredInAnimator;
        GyrussEventManager.PlayerShipSpawnInitiated += SpawnPlayerShip;
        GyrussEventManager.PlayerLivesGetInitiated += GetPlayerLives;
        GyrussEventManager.PlayerRocketsGetInitiated += GetPlayerRockets;
        GyrussEventManager.PlayerSpawnedToggleInitiated += TogglePlayerSpawned;
        GyrussEventManager.MovePlayerToWarpPositionInitiated += MoveShipToWarpingPosition;
        GyrussEventManager.WarpingEffectsToggleInitiated += ToggleWarpingEffects;
        GyrussEventManager.WarpingPlayerInitiated += WarpPlayer;
        GyrussEventManager.MovePlayerToCenterPointInitiated += MoveShipToCenterPoint;
        GyrussEventManager.PlayerKillInitiated += KillPlayer;
        GyrussEventManager.RocketShootInitiated += ShootRocket;
        GyrussEventManager.RocketReloadInitiated += ReloadRocket;
    }

    private void RotateShip(Vector3 rotateAxis)
    {
        playerShip.transform.RotateAround(Vector3.zero, rotateAxis, Time.deltaTime * speed);
    }

    private void ShootBullet()
    {
        //TODO: add shooting effect near ship

        if (!(reload >= 0.3f)) {
            if (shootingPointSingleGO.GetComponent<SpriteRenderer>().enabled) {
                shootingPointSingleGO.GetComponent<SpriteRenderer>().enabled = false;
            }
            
            if (shootingPointDoubleGO.GetComponent<SpriteRenderer>().enabled) {
                shootingPointDoubleGO.GetComponent<SpriteRenderer>().enabled = false;
            }
            
            return;
        }
        reload = 0;

        if (!doubleBulletMode) {
            Instantiate(bulletPrefabSingle, shootingPointSingle.position, playerShip.transform.rotation, playerBulletPool);
            shootingPointSingleGO.GetComponent<SpriteRenderer>().enabled = true;
        }
        else {
            Instantiate(bulletPrefabDouble, shootingPointSingle.position, playerShip.transform.rotation,
                playerBulletPool);
            shootingPointDoubleGO.GetComponent<SpriteRenderer>().enabled = true;
        }
        
        if (Input.GetKeyUp(KeyCode.Space)) { reload = 1; }
    }

    private void SpawnPlayerShip()
    {
        playerShip.transform.position = playerStartingPosition;
        playerShip.SetActive(true);
        playerShip.transform.rotation = Quaternion.identity;
        playerAnimator.SetBool(Entered, true);
        
        GyrussGameManager.Instance.SetConditionInTimer("playerEnteredStage", true);
    }

    private void SetPlayerEnteredInAnimator(bool entered)
    {
        playerAnimator.SetBool(Entered, entered);
    }
    
    private Vector3 GetPlayerPosition()
    {
        return playerShip.transform.position;
    }

    private int GetPlayerLives()
    {
        return lives;
    }

    private int GetPlayerRockets()
    {
        return rockets;
    }
    
    private void ToggleShootingMode()
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

    private bool MoveShipToWarpingPosition()
    {
        if (playerAnimator.GetBool(Warping)) return true;
        
        RotateShip(Vector3.forward);
        
        Vector3 currPos = playerShip.transform.position;

        float condition = currPos.y - playerStartingPosition.y;

        if (!(condition < 0.01)) return false;
        
        playerShip.transform.position = playerStartingPosition;
        playerShip.transform.rotation = Quaternion.identity;
        return true;
    }

    private void TogglePlayerSpawned()
    {
        spawned = !spawned;
    }

    private void ToggleWarpingEffects()
    {
        warpingEffects.SetActive(!warpingEffects.activeSelf);
    }

    private bool MoveShipToCenterPoint()
    {
        Vector3 currPos = playerShip.transform.position;
        
        playerShip.transform.position =
            Vector3.MoveTowards(currPos, Vector3.zero, Time.deltaTime);

        return currPos == Vector3.zero;
    }

    private void KillPlayer()
    {
        lives--;
        
        GyrussGameManager.Instance.SetDeathParticlesOnPosition();
        playerShip.SetActive(false);
        GyrussGameManager.Instance.PrepareDeathParticles();
        
        if (lives < 0) {
            Debug.LogError("game ends here");
        }
        else { 
            GyrussGameManager.Instance.DecreaseGUILives(lives);
            GyrussGameManager.Instance.SetConditionInTimer("playerRespawn", true);
        }
    }

    private void WarpPlayer()
    {
        if (playerAnimator.GetBool(Warping)) return;
        playerAnimator.SetBool(Warping, true);
        ToggleWarpingEffects();
        GyrussGameManager.Instance.SetConditionInTimer("warping", true);
    }

    private void ShootRocket()
    {
        Instantiate(rocketPrefab, shootingPointSingle.position, playerShip.transform.rotation, playerBulletPool);
        shootRocket = true;
        GyrussGameManager.Instance.DecreasePlayerRockets(--rockets);
        GyrussGameManager.Instance.SetConditionInTimer("rocketReload", true);
    }

    private void ReloadRocket()
    {
        shootRocket = false;
    }
    
}
