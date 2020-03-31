using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStates currentEnemyState = EnemyStates.entering;
    public PathFollow pathFollow;
    public PathsDatabase pathsDatabase;
    public float timeToWait = 0;
    private bool pathAssignedIn = false;
    private bool pathAssignedBack = false;

    private float speed;
    
    private int spotIndex;
    private Vector3 centerPosition;

    private void Start()
    {
        speed = pathFollow.speed;
    }

    private void Update() {
        switch(currentEnemyState){
            case EnemyStates.entering:
                centerPosition = GyrussGameManager.Instance.OccupyEnemySpot(spotIndex);
                Debug.Log(centerPosition);
                
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
        }   
    }

    private void enterScreen(){
        if(!pathFollow.endPathReached()) {
            pathFollow.moveOnPath();
        } else { currentEnemyState = EnemyStates.fly_to_spot; }
    }

    private void flyToSpot(){
        if (!pathFollow.endPathReached()) return;
        
        Vector3.MoveTowards(transform.position, centerPosition, speed);

        if (transform.position == centerPosition) {
            Debug.Log("here");
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
}
