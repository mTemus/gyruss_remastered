using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyStates currentEnemyState = EnemyStates.entering;

    private void Update() {
        switch(currentEnemyState){
            case EnemyStates.entering:
                enterScreen();
            break;
            case EnemyStates.fly_to_spot:
                flyToTheCenter();
            break;
            case EnemyStates.wait:
                waitInTheMiddle();
            break;
            case EnemyStates.attack:
                attackPlayer();
            break;
            case EnemyStates.fly_away:
                flyAway();
            break;
        }   
    }

    private void enterScreen(){

    }

    private void flyToTheCenter(){

    }

    private void waitInTheMiddle(){

    }

    private void attackPlayer(){

    }

    private void flyAway(){

    }
}
