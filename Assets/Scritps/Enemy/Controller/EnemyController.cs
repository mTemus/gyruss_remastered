﻿using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PathFollow pathFollow;
    public PathsDatabase pathsDatabase;
    public float timeToWait = 0;
    
    private bool pathAssignedIn = false;
    private bool pathAssignedBack = false;
    
    private int spotIndex;
    private int waveId;
    
    private float speed;

    private GameObject myModule;
    
    private EnemyStates myCurrentState = EnemyStates.no_state;
    private StageType currentStageType = StageType.no_type;
    
    private Vector3 modulePosition;
    private Vector3 centerPosition;
    
    private void Start()
    {
        speed = pathFollow.speed;
    }

    private void Update() {
        switch(myCurrentState){
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
                if (currentStageType == StageType.mini_boss) {
                    transform.GetComponent<PositionsUpadator>().MyEnemyController = this;
                }
                else {
                    Transform myCenterPoint = GyrussGameManager.Instance.OccupyEnemySpot(spotIndex);
                    
                    PositionsUpadator myUpdator = transform.GetComponent<PositionsUpadator>();
                    myUpdator.SetPointToUpdate(myCenterPoint);
                    myUpdator.MyEnemyController = this;
                
                    centerPosition = myCenterPoint.position;
                }
                
                myCurrentState = EnemyStates.entering;
                break;
            
            case EnemyStates.fly_to_mini_boss:
                transform.position = Vector3.MoveTowards(transform.position, modulePosition, speed * Time.deltaTime);

                if (transform.position == modulePosition) {
                    GyrussGameManager.Instance.RemoveShipFromAwaitingList(myModule, transform.gameObject);
                    transform.position = new Vector3(100, 100, 0);
                    myModule.GetComponent<MiniBossModuleController>().EatEnemyShip(transform.gameObject);
                    myCurrentState = EnemyStates.no_state;
                }
                break;
            
            case EnemyStates.fly_from_mini_boss:
                break;
            
            case EnemyStates.die:
                if (currentStageType == StageType.mini_boss) {
                    myModule.GetComponent<MiniBossModuleController>().DeleteEatenShip(transform.gameObject);
                    GyrussGameManager.Instance.RemoveShipFromAwaitingList(myModule, transform.gameObject);
                }
                
                GyrussGameManager.Instance.CreateExplosion(transform.position);
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
        else 
            myCurrentState = currentStageType == StageType.mini_boss ? 
            EnemyStates.fly_to_mini_boss : EnemyStates.fly_to_spot; 
    }

    private void flyToSpot(){
        // if (!pathFollow.endPathReached()) return; <- to blokuje latanie do punktu

        transform.position = Vector3.MoveTowards(transform.position, centerPosition, Time.deltaTime * speed);

        if (transform.position == centerPosition) {
            myCurrentState = EnemyStates.wait;
        }
    }

    private void waitInTheMiddle(){
        if(timeToWait >= 5f){
            myCurrentState = EnemyStates.attack;
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
        if (myCurrentState == EnemyStates.die) return;

        if (other.CompareTag("PlayerBullet")) {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            myCurrentState = EnemyStates.die;
            Destroy(other.transform.parent.gameObject);
        }

        if (other.CompareTag("Rocket")) {
            myCurrentState = EnemyStates.die;
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
