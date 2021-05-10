using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Ship parts")]
    [SerializeField] private GameObject playerShip = null;
    [SerializeField] private GameObject shootingPointSingleGO = null;
    [SerializeField] private GameObject shootingPointDoubleGO = null;
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private GameObject warpingEffects = null;
    
    [Header("Prefabs")] 
    [SerializeField] private GameObject bulletPrefabSingle = null;
    [SerializeField] private GameObject bulletPrefabDouble = null;
    [SerializeField] private GameObject rocketPrefab = null;
    
    [Header("Map")]
    [SerializeField] private SpriteRenderer background = null;

    [Header("Pools")] 
    [SerializeField] private Transform playerBulletPool = null;
    
    [Header("Android input")]
    [SerializeField] private FixedJoystick fixedJoystick = null;
    [SerializeField] private FixedButton fixedButtonBullet = null;
    [SerializeField] private FixedButton fixedButtonRocket = null;
    
    
    private float speed = 150f;
    private float reload = 1;
    
    private bool doubleBulletMode = false;
    private bool spawned;
    private bool shootRocket;

    private int lives = 3;
    private int rockets = 1;

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
    }

    private void Update()
    {
        if (!spawned) return;
        reload += Time.deltaTime;

        RotateShip(Vector3.forward * fixedJoystick.Horizontal * speed);

        if (fixedButtonBullet.Pressed) {
            ShootBullet();
        }

        if (fixedButtonRocket.Pressed) {
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
        GyrussEventManager.LifeAddInitiated += AddLife;
        GyrussEventManager.ShootingModeToggleInitiated += ToggleShootingMode;
        GyrussEventManager.RocketAddInitialized += AddRocket;
        GyrussEventManager.GetPlayerShipStartingPositionInitiated += GetPlayerStartingPosition;
    }

    private void RotateShip(Vector3 rotateAxis)
    {
        playerShip.transform.RotateAround(Vector3.zero, rotateAxis, Time.deltaTime * speed);
    }

    private void ShootBullet()
    {
        if (!(reload >= 0.25f)) {
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
        playerAnimator.SetBool(Warping, false);
        playerAnimator.SetBool(Entered, true);
        playerShip.transform.localScale = new Vector3(1,1,0);
        
        GyrussGameManager.Instance.SetConditionInTimer("playerEnteredStage", true);
    }

    private void SetPlayerEnteredInAnimator(bool entered)
    {
        if (playerAnimator == null) { return; }
        playerAnimator.SetBool(Entered, entered);
    }
    
    private Vector3 GetPlayerPosition()
    {
        return playerShip == null ? Vector3.zero : playerShip.transform.position;
    }

    private Vector3 GetPlayerStartingPosition()
    {
        return playerStartingPosition;
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

        Transform playerTransform = playerShip.transform;
        Vector3 currPos = playerTransform.position;
        
        RotateShip(currPos.x < 0 ? Vector3.forward : Vector3.back);
        
        float condition = currPos.y - playerStartingPosition.y;

        if (!(condition < 0.01)) return false;
        
        playerTransform.position = playerStartingPosition;
        playerTransform.rotation = Quaternion.identity;
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
        GyrussGameManager.Instance.PlaySoundEffect("player-death");
        GyrussGameManager.Instance.PrepareDeathParticles();
        
        if (lives < 0) {
            GyrussGameManager.Instance.StopTimer("weaponBonusSpawn");
            GyrussGameManager.Instance.StopTimer("rocketBonusSpawn");
            GyrussGameManager.Instance.StopTimer("asteroidSpawn");
            GyrussGameManager.Instance.SetLevelState(LevelState.wait);
            GyrussGameManager.Instance.SetStageState(StageState.wait);

            GyrussGameManager.Instance.SetConditionInTimer("gameOver", true);
            Destroy(playerShip);
        }
        else { 
            GyrussGameManager.Instance.DecreaseGUILives(lives);
            GyrussGameManager.Instance.SetConditionInTimer("playerRespawn", true);
            
            float randomRespawn = Random.Range(20, 30);
            GyrussGameManager.Instance.SetPeriodInTimer("weaponBonusSpawn", randomRespawn);
            GyrussGameManager.Instance.SetConditionInTimer("weaponBonusSpawn", true);
        }

        if (!doubleBulletMode) return;
        ToggleShootingMode();
    }

    private void WarpPlayer()
    {
        if (playerAnimator.GetBool(Warping)) return;
        playerAnimator.SetBool(Warping, true);
        ToggleWarpingEffects();
        GyrussGameManager.Instance.TogglePlayerSpawned();
        GyrussGameManager.Instance.SetConditionInTimer("warping", true);
        GyrussGameManager.Instance.PlaySoundEffect("warp-stage");
    }

    private void ShootRocket()
    {
        Instantiate(rocketPrefab, shootingPointSingle.position, playerShip.transform.rotation, playerBulletPool);
        shootRocket = true;
        rockets--;
        GyrussGameManager.Instance.DecreasePlayerRockets(rockets);
        GyrussGameManager.Instance.SetConditionInTimer("rocketReload", true);
    }

    private void AddLife()
    {
        lives++;
        GyrussGameManager.Instance.PlaySoundEffect("additionalLife");
        GyrussGameManager.Instance.IncreaseLifeIcons();
    }

    private void AddRocket()
    {
        rockets++;
        GyrussGameManager.Instance.PlaySoundEffect("rocketIcon-initialize");
        GyrussGUIEventManager.OnRocketsIconsIncreaseInitiated();
    }

    private void ReloadRocket()
    {
        shootRocket = false;
    }

    public bool DoubleBulletMode => doubleBulletMode;
}
