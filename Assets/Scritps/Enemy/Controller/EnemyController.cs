using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStates currentEnemyState = EnemyStates.no_state;
    public PathFollow pathFollow;
    public PathsDatabase pathsDatabase;
    public float timeToWait = 0;
    
    private bool pathAssignedIn = false;
    private bool pathAssignedBack = false;
    
    private int spotIndex;
    
    private float speed;
    
    
    private Vector3 centerPosition;
    
    private void Start()
    {
        speed = pathFollow.speed;
    }

    private void Update() {
        switch(currentEnemyState){
            case EnemyStates.entering:
                randomizePath();
                enterScreen();
            break;

            case EnemyStates.fly_to_spot:
                flyToSpot();
            break;
            
            case EnemyStates.wait:
                // waitInTheMiddle();
                UpdateCenterPosition();
                
            break;
            
            case EnemyStates.attack:
                randomizePathBack();
                attackPlayer();
            break;
            
            case EnemyStates.fly_away:
                flyAway();
            break;
            
            case EnemyStates.no_state:
                break;
            
            case EnemyStates.take_a_spot:
                Transform myCenterPoint = GyrussGameManager.Instance.OccupyEnemySpot(spotIndex);
                CenterPointUpdator myUpdator = transform.GetComponent<CenterPointUpdator>();
                
                myUpdator.CenterPoint = myCenterPoint;
                myUpdator.MyEnemyController = this;
                
                centerPosition = myCenterPoint.position;
                currentEnemyState = EnemyStates.entering;
                
                break;
            case EnemyStates.fly_to_mini_boss:
                break;
            
            case EnemyStates.fly_from_mini_boss:
                break;
            
            case EnemyStates.die:
                GyrussGameManager.Instance.CreateExplosion(transform.position);
                Destroy(transform.gameObject);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }   
    }

    private void enterScreen(){
        if(!pathFollow.endPathReached()) {
            pathFollow.moveOnPath();
        }
        else {
            currentEnemyState = EnemyStates.fly_to_spot;
        }
    }

    private void flyToSpot(){
        // if (!pathFollow.endPathReached()) return; <- to blokuje latanie do punktu

        transform.position = Vector3.MoveTowards(transform.position, centerPosition, Time.deltaTime * speed);

        if (transform.position == centerPosition) {
            currentEnemyState = EnemyStates.wait;
        }
    }

    private void waitInTheMiddle(){
        if(timeToWait >= 5f){
            currentEnemyState = EnemyStates.attack;
        }else{
            timeToWait+=Time.deltaTime;
        }
    }

    private void attackPlayer(){
        if(!pathFollow.endPathReached()){
            pathFollow.moveOnPath();
        }
    }

    private void flyAway(){

    }

    private void randomizePath(){
        if(pathAssignedIn == false){
            pathFollow.pathCreator = pathsDatabase.getRandomPathIn();
            pathFollow.distanceTravelled = 0f;
            pathAssignedIn = true;
        }
    }

    private void randomizePathBack(){
        if(pathAssignedBack == false){
            pathFollow.pathCreator = pathsDatabase.getRandomPathBack();
            pathFollow.distanceTravelled = 0f;
            pathAssignedBack = true;
        }
    }
    
    private void OnDestroy()
    {
        GyrussGameManager.Instance.KillEnemy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentEnemyState == EnemyStates.die) return;
        if (!other.CompareTag("PlayerBullet")) return;

        if (other.CompareTag("PlayerBullet")) {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            currentEnemyState = EnemyStates.die;
            Destroy(other.transform.parent.gameObject);
        }

        if (other.CompareTag("Rocket")) {
            currentEnemyState = EnemyStates.die;
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
    
}
