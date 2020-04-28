using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int spotIndex;
    private int waveId;
    private float speed;
    private float internalTimer;
    private float randomWaitTime;
    private float attackChance;
    private bool pathBackAssigned = false;
    public bool pathAttackAssigned = false;
    private bool waitReached = false;
    private bool attacked = false;

    public PathFollow pathFollow;
    private GameObject myModule;
    private EnemyStates myCurrentState = EnemyStates.no_state;
    private StageType currentStageType = StageType.no_type;
    
    private Vector3 modulePosition;
    private Vector3 centerPosition;
    
    private void Start()
    {
        speed = pathFollow.speed;
        pathFollow.pathCreator = GyrussGameManager.Instance.GetCurrentEnemyPathIn();
        randomWaitTime = UnityEngine.Random.Range(2,20);
        attackChance = UnityEngine.Random.Range(0,100);
    }

    private void FixedUpdate() {
        if(waitReached){
            internalTimer += Time.deltaTime;
        }
    }

    private void Update() {
        switch(myCurrentState){
            case EnemyStates.entering:
                enterScreen();
                break;

            case EnemyStates.fly_to_spot:
                flyToSpot();
                break;
            
            case EnemyStates.wait:
                UpdateCenterPosition();
                if(internalTimer >= randomWaitTime){
                    myCurrentState = EnemyStates.attack;
                    waitReached = false;
                }
                break;
            
            case EnemyStates.attack:
                attackPlayer();
                break;
            
            case EnemyStates.fly_away:
                flyAway();
                break;
            
            case EnemyStates.no_state:
                break;
            
            case EnemyStates.take_a_spot:
                switch (currentStageType) {
                    case StageType.chance:
                        myCurrentState = EnemyStates.entering;
                        return;
                    
                    case StageType.mini_boss:
                        transform.GetComponent<PositionsUpadator>().MyEnemyController = this;
                        break;
                    
                    default:
                        Transform myCenterPoint = GyrussGameManager.Instance.OccupyEnemySpot(spotIndex);
                    
                        PositionsUpadator myUpdator = transform.GetComponent<PositionsUpadator>();
                        myUpdator.SetPointToUpdate(myCenterPoint);
                        myUpdator.MyEnemyController = this;
                
                        centerPosition = myCenterPoint.position;
                        break;
                }
                
                myCurrentState = EnemyStates.entering;
                break;
            
            case EnemyStates.fly_to_mini_boss:
                transform.position = Vector3.MoveTowards(transform.position, modulePosition, speed * Time.deltaTime);

                if (transform.position == modulePosition) {
                    GyrussGameManager.Instance.RemoveShipFromAwaitingList(myModule, transform.gameObject);
                    myModule.GetComponent<MiniBossModuleController>().EatEnemyShip(transform.gameObject);
                    myCurrentState = EnemyStates.no_state;
                }
                break;
            
            case EnemyStates.fly_from_mini_boss:
                break;
            
            case EnemyStates.die:
                switch (currentStageType) {
                    case StageType.mini_boss:
                        myModule.GetComponent<MiniBossModuleController>().DeleteEatenShip(transform.gameObject);
                        GyrussGameManager.Instance.RemoveShipFromAwaitingList(myModule, transform.gameObject);
                        break;
                    case StageType.chance:
                        GyrussGameManager.Instance.KillEnemyInChanceStage();
                        break;
                }

                bool enemyInCenter = transform.GetComponent<ScalingController>().EnemyIsInCenterPosition;
                GyrussGameManager.Instance.PlaySoundEffect(!enemyInCenter ? "enemyShipDeath-inFly" : "enemyShipDeath-center");
                
                GyrussGameManager.Instance.CreateExplosion(transform.position, "normal");
                GyrussGameManager.Instance.AddPointsToScore(100);
                GyrussGameManager.Instance.CheckBonusPointsForWaveKill(this);
                Destroy(transform.gameObject);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void enterScreen(){
        if(!pathFollow.endPathReached()) 
            pathFollow.moveOnPath();
        else {
            switch (CurrentStageType) {
                case StageType.first_stage:
                    myCurrentState = EnemyStates.fly_to_spot;
                    break;
                
                case StageType.mini_boss:
                    myCurrentState = EnemyStates.fly_to_mini_boss;
                    break;
                
                case StageType.boss:
                    myCurrentState = EnemyStates.fly_to_spot;
                    break;
                
                case StageType.chance:
                    transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }      
    }

    private void flyToSpot(){
        transform.position = Vector3.MoveTowards(transform.position, centerPosition, Time.deltaTime * speed);

        if (transform.position == centerPosition) {
            myCurrentState = EnemyStates.wait;
            waitReached = true;
        }
    }

    private void attackPlayer(){
        if(attackChance > 30 && pathAttackAssigned == false){
            pathAttackAssigned = true;
            pathFollow.pathCreator = GyrussGameManager.Instance.getClosestPathOut();
            pathFollow.distanceTravelled = 0;
        }else if(attackChance <= 30){
            myCurrentState = EnemyStates.fly_away;
        }else{
            if(!pathFollow.endPathReached()){
                pathFollow.moveOnPath();
            }else{
                Destroy(transform.gameObject);
            }
        }
    }

    private void flyAway(){
        if(pathBackAssigned == false){
            pathBackAssigned = true;
            pathFollow.pathCreator = GyrussGameManager.Instance.GetCurrentEnemyPathOut();
            pathFollow.distanceTravelled = 0;
        }else{
            if(!pathFollow.endPathReached()){
                pathFollow.moveOnPath();
            }else{
                Destroy(transform.gameObject);
            }
        }
    }
    
    private void OnDestroy()
    {
        GyrussGameManager.Instance.KillEnemy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myCurrentState == EnemyStates.die) return;

        if (other.CompareTag("PlayerBullet")) {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            myCurrentState = EnemyStates.die;
            Destroy(other.transform.parent.gameObject);
        }

        if (other.CompareTag("Rocket")) {
            myCurrentState = EnemyStates.die;
        }

        if(other.CompareTag("Player") && pathAttackAssigned){
            Destroy(transform.gameObject);
            GyrussGameManager.Instance.KillPlayer();
        }
    }

    private void UpdateCenterPosition()
    {
        transform.position = centerPosition;
    }
    
    
    public int SpotIndex
    {
        get => spotIndex;
        set => spotIndex = value;
    }

    public Vector3 CenterPosition
    {
        get => centerPosition;
        set => centerPosition = value;
    }

    public int WaveId
    {
        get => waveId;
        set => waveId = value;
    }

    public EnemyStates CurrentEnemyState
    {
        get => myCurrentState;
        set => myCurrentState = value;
    }

    public StageType CurrentStageType
    {
        get => currentStageType;
        set => currentStageType = value;
    }

    public Vector3 ModulePosition
    {
        get => modulePosition;
        set => modulePosition = value;
    }

    public GameObject MyModule
    {
        get => myModule;
        set => myModule = value;
    }
}
