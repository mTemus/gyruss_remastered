using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniBossModuleController : MonoBehaviour
{
    [Header("Module properties")]
    [SerializeField] private int lifeAmount = 60;
    [SerializeField] private float openTime = 2;

    private static readonly int Open = Animator.StringToHash("open");
    private static readonly int Hurt = Animator.StringToHash("hurt");

    private bool attack = true;

    private float timer;
    private float period;
    private float closeTime;
    
    private Animator myAnimator;    
    
    private List<GameObject> eatenShips;
        
    void Start()
    {
        eatenShips = new List<GameObject>();
        myAnimator = transform.GetComponent<Animator>();
        closeTime = Random.Range(7, 20);
        period = closeTime;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (!myAnimator.GetBool(Open) && attack) {
            CountdownToOpen();
        } else if (myAnimator.GetBool(Open) && attack) {
            CountdownToClose();
        }
    }

    private void CountdownToOpen()
    {
        if (timer >= period) {
            myAnimator.SetBool(Open, true);
            timer = 0;
            period = openTime;
            
            // SpitEnemyShip();
        }

        timer += Time.deltaTime;
    }

    private void CountdownToClose()
    {
        if (timer >= period) {
            myAnimator.SetBool(Open, false);
            timer = 0;
            period = closeTime;
        }
        
        timer += Time.deltaTime;
    }
    
    private void SpitEnemyShip()
    {
        if (eatenShips.Count <= 0) return;
        GameObject spittedShip = eatenShips[eatenShips.Count - 1];

        spittedShip.transform.position = transform.position;
        spittedShip.GetComponent<EnemyController>().CurrentEnemyState = EnemyStates.fly_from_mini_boss;
    }
    
    private void SetHurtFalse()
    {
        myAnimator.SetBool(Hurt, false);
    }

    private void GetHurt()
    {
        lifeAmount -= 1;
        GyrussGameManager.Instance.PlaySoundEffect("module-hurt");
        if (lifeAmount == 0) { Die(); }
    }

    private void Die()
    {
        foreach (GameObject ship in eatenShips) {
            Destroy(ship);
        }
        
        GyrussGameManager.Instance.KillEnemy();
        GyrussGameManager.Instance.AddPointsToScore(800);
        GyrussGameManager.Instance.CreateExplosion(transform.position, "miniBoss");
        GyrussGameManager.Instance.KillMiniBossModule(transform.gameObject);
        GyrussGameManager.Instance.PlaySoundEffect("module-explosion");
        Destroy(transform.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag) {
            case "EnemyShip":
                Debug.LogWarning("ship arrived");
                myAnimator.SetBool(Open, true);
                attack = false;
                break;
            
            case "Rocket":
                Die();
                break;
            
            case "PlayerBullet":
                if (myAnimator.GetBool(Hurt)) {
                    myAnimator.CrossFade(myAnimator.GetBool(Open) ? "open_hurt" : "closed_hurt", 0f, -1, 0f);
                }
                myAnimator.SetBool(Hurt, true);
                GetHurt();
                break;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag) {
            case "EnemyShip":
                myAnimator.SetBool(Open, false);
                attack = true;
                break;
        }
    }
    
    public void EatEnemyShip(GameObject ship)
    {
        if (!eatenShips.Contains(ship)) {
            eatenShips.Add(ship);
        }
        else {
            Debug.LogError("Ship can't be eaten two times!!! " + transform.name);
        }
        
    }

    public void DeleteEatenShip(GameObject ship)
    {
        if (eatenShips.Contains(ship)) 
            eatenShips.Remove(ship); 
    }
}
