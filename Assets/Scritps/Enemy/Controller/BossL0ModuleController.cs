using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossL0ModuleController : MonoBehaviour
{
    [Header("Module properties")]
    [SerializeField] private int life = 70;

    [Header("Module timers")] 
    [SerializeField] private float openTime = 2f;
     private float closeTime;

    [Header("Prefabs")] 
    [SerializeField] private GameObject bulletPrefab;

    private float periodTimer;
    private float counterTimer;
    private bool opened;
    private bool shooted;
    
    private Animator myAnimator;
    private static readonly int Open = Animator.StringToHash("open"); 
    private static readonly int Hurt = Animator.StringToHash("hurt");

    void Start()
    {
        myAnimator = transform.GetComponent<Animator>();
        counterTimer = closeTime;
        closeTime = Random.Range(4, 10);
    }

    private void Update()
    {
        if (!myAnimator.GetBool(Open)) {
            if (counterTimer <= 0) {
                myAnimator.SetBool(Open, true);
                opened = true;
                counterTimer = openTime;
            }

            counterTimer -= Time.deltaTime;
        }
        else if (myAnimator.GetBool(Open)) {
            if (counterTimer <= openTime - 0.2f && !shooted) {
                Shoot();
                shooted = true;
            }

            if (counterTimer <= 0) {
                myAnimator.SetBool(Open, false);
                opened = false;
                shooted = false;
                counterTimer = closeTime;
            }
            counterTimer -= Time.deltaTime;
        }
    }


    private void SetHurtFalse()
    {
        myAnimator.SetBool(Hurt, false);
    }
    
    private void GetHurt()
    {
        life--;
        GyrussGameManager.Instance.PlaySoundEffect("module-hurt");
        
        if (life != 0) return;
        Die();
    }

    private void Die()
    {
        GyrussGameManager.Instance.CreateExplosion(transform.position, "miniBoss");
        GyrussGameManager.Instance.KillBossModule();
        GyrussGameManager.Instance.PlaySoundEffect("module-explosion");
        Destroy(transform.gameObject);
    }

    private void Shoot()
    {
        ModuleShootingDirection direction = CalculateShootingDirection();
        
        SpawnBullet(CalculateBulletTargetPosition(-0.8f, direction));
        SpawnBullet(CalculateBulletTargetPosition(0, direction));
        SpawnBullet(CalculateBulletTargetPosition(0.8f, direction));
    }

    private void SpawnBullet(Vector3 bulletPosition)
    {
        Transform moduleTransform = transform;
        GameObject bullet = Instantiate(bulletPrefab, moduleTransform.position, Quaternion.identity, moduleTransform);
        bullet.transform.position = transform.position;
        bullet.GetComponent<EnemyBulletController>().StartBullet(bulletPosition);
    }

    private ModuleShootingDirection CalculateShootingDirection()
    {
        Vector3 position = transform.position;

        if (position.x > 0 && position.y == 0) {
            return ModuleShootingDirection.right;
        }

        if (position.x == 0 && position.y < 0) {
            return ModuleShootingDirection.down;
        }

        if (position.x < 0 && position.y == 0) {
            return ModuleShootingDirection.left;
        }

        if (position.x == 0 && position.y > 0) {
            return ModuleShootingDirection.up;
        }

        return ModuleShootingDirection.up;
    }

    private Vector3 CalculateBulletTargetPosition(float offset, ModuleShootingDirection direction)
    {
        Vector3 position = Vector3.one;
        
        switch (direction) {
            case ModuleShootingDirection.up:
                position = new Vector3(offset, 5, 0);
                break;
            
            case ModuleShootingDirection.down:
                position = new Vector3(offset, -5, 0);
                break;
            
            case ModuleShootingDirection.left:
                position = new Vector3(-5, offset, 0);
                break;
            
            case ModuleShootingDirection.right:
                position = new Vector3(5, offset, 0);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        return position;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag) {
            case "Rocket":
                Die();
                break;
            
            case "PlayerBullet":
                if (!myAnimator.GetBool(Open)) {
                    GyrussGameManager.Instance.PlaySoundEffect("module-notHurt");
                    Destroy(other.gameObject);
                    return;
                }
                
                if (!opened) return;
                if (myAnimator.GetBool(Hurt)) {
                    myAnimator.CrossFade("Module_open_hurt", 0f, -1, 0f);
                }
                
                myAnimator.SetBool(Hurt, true);
                GetHurt();
                Destroy(other.gameObject);
                break;
        }
    }
}
