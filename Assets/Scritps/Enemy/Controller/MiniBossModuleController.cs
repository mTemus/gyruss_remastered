using System;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossModuleController : MonoBehaviour
{
    [Header("Module properties")]
    [SerializeField] private int lifeAmount;
    [SerializeField] private float closeTime;
    [SerializeField] private float openTime;

    private Animator myAnimator;    
    
    private List<GameObject> eatenShips;
    private static readonly int Open = Animator.StringToHash("open");
    private static readonly int Hurt = Animator.StringToHash("hurt");

    // Start is called before the first frame update
    void Start()
    {
        eatenShips = new List<GameObject>();

        myAnimator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpitEnemyShip()
    {
        if (eatenShips.Count > 0) {
            GameObject spittedShip = eatenShips[eatenShips.Count - 1];
            
        }
        
    }

    public void EatEnemyShip(GameObject ship)
    {
        eatenShips.Add(ship);
        ship.transform.position = new Vector3(100, 100, 0);
        ship.GetComponent<EnemyController>().CurrentEnemyState = EnemyStates.no_state;
        
        Debug.Log(transform.name + " has eaten: " + ship.name);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag) {
            case "EnemyShip":
                Debug.Log("ship arrived");
                myAnimator.SetBool(Open, true);
                break;
            
            case "PlayerBullet":
                myAnimator.SetBool(Hurt, true);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag) {
            case "EnemyShip":
                myAnimator.SetBool(Open, false);
                break;
            
            case "PlayerBullet":
                
                break;
        }
    }
}
