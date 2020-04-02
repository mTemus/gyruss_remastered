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

    private float speed;
    
    private int spotIndex;
    private float deathCounter = 0;
    
    private Vector3 centerPosition;
    private static readonly int Dead = Animator.StringToHash("dead");

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
                waitInTheMiddle();
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
                centerPosition = GyrussGameManager.Instance.OccupyEnemySpot(spotIndex);
                currentEnemyState = EnemyStates.entering;
                
                break;
            case EnemyStates.fly_to_mini_boss:
                break;
            case EnemyStates.fly_from_mini_boss:
                break;
            case EnemyStates.die:

                if (deathCounter == 0) { transform.GetComponent<Animator>().SetBool(Dead, true); }
                else if (deathCounter >= 0.4f) {
                    Destroy(transform.gameObject);
                }

                deathCounter += Time.deltaTime;
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

    private void Die() // <-- funkcja do animacji
    {
        Destroy(transform.gameObject);
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


    private void OnDestroy()
    {
        GyrussGameManager.Instance.KillEnemy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (currentEnemyState == EnemyStates.die) return;
        if (!other.CompareTag("PlayerBullet")) return;
        
        transform.GetComponent<BoxCollider2D>().enabled = false;
        currentEnemyState = EnemyStates.die;
    }
}
