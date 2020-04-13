using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header("Player Ship")]
    [SerializeField] private GameObject playerShip;
    
    [Header("Effects Prefabs")]
    [SerializeField] private GameObject ExplosionPrefab;

    [Header("Pools")] 
    [SerializeField] private Transform explosionPool;
    
    [Header("Particles")]
    [SerializeField] private GameObject[] reviveParticles;
    [SerializeField] private GameObject[] deathParticles;
    [SerializeField] private GameObject[] rocketParticles;
    
    private List<GameObject> readyReviveParticles;
    private Vector3 playerShipPosition;

    private int rocketParticleId;
    private int rocketParticleCondition;
    
    private void Start()
    {
        readyReviveParticles = new List<GameObject>();
        SetDelegates();
    }

    private void SetDelegates()
    {
        GyrussEventManager.PlayerShipPositionSetupInitiated += SetPlayerShipPosition;
        GyrussEventManager.ReviveParticleRegistrationInitiated += RegisterReviveParticle;
        GyrussEventManager.ReviveParticlesPreparationInitiated += PrepareReviveParticles;
        GyrussEventManager.ExplosionCreationInitiated += CreateExplosion;
        GyrussEventManager.DeathParticlesOnPositionsSetupInitiated += SetDeathParticlesOnPositions;
        GyrussEventManager.DeathParticlesPreparationInitiated += PrepareDeathParticles;
        GyrussEventManager.RocketParticlesOnPositionsSetupInitiated += SetRocketParticlesOnPositions;
        GyrussEventManager.RocketParticlesPreparationInitiated += PrepareRocketParticles;
    }
    
    private void SetReviveParticlesOnPositions()
    {
        Vector3 originPosition = playerShipPosition + new Vector3(0, 2, 0);
        int angle = 0;

        foreach (GameObject reviveParticle in reviveParticles) {
            reviveParticle.transform.position = originPosition;
            reviveParticle.transform.RotateAround(playerShipPosition, Vector3.forward, angle);

            angle += 360 / reviveParticles.Length;
        }
    }

    private void SetDeathParticlesOnPositions()
    {
        Vector3 originPosition = playerShip.transform.position + new Vector3(0, 1.5f, 0);
        int angle = 0;
        
        foreach (GameObject deathParticle in deathParticles) {
            Transform deathParticleTransform = deathParticle.transform;
            Transform playerShipTransform = playerShip.transform;
            
            deathParticle.transform.position = originPosition;
            deathParticle.transform.RotateAround(playerShip.transform.position, Vector3.forward, angle);
            deathParticle.GetComponent<DeathParticleController>().SetDeathPosition(deathParticle.transform.position);
            deathParticleTransform.position = playerShipTransform.position;

            angle += 360 / deathParticles.Length;
        }
    }

    private void PrepareDeathParticles()
    {
        foreach (GameObject deathParticle in deathParticles) {
            deathParticle.SetActive(true);
            deathParticle.GetComponent<DeathParticleController>().StartParticle();
        }
    }

    private void SetPlayerShipPosition(Vector3 newPlayerShipPosition)
    {
        playerShipPosition = newPlayerShipPosition;
        SetReviveParticlesOnPositions();
    }

    private void RegisterReviveParticle(GameObject reviveParticle)
    {
        if (readyReviveParticles.Contains(reviveParticle)) return;
        
        readyReviveParticles.Add(reviveParticle);

        if (readyReviveParticles.Count == 8) {
            GyrussGameManager.Instance.SpawnPlayerShip();
            
            foreach (GameObject particle in reviveParticles) {
                particle.SetActive(false);
            }
            
            SetReviveParticlesOnPositions();
            
            readyReviveParticles = new List<GameObject>();
        }
    }

    private void PrepareReviveParticles()
    {
        foreach (GameObject reviveParticle in reviveParticles) {
            reviveParticle.SetActive(true);
            reviveParticle.GetComponent<ReviveParticleController>().PrepareMovement(playerShipPosition);
        }
    }

    private void SetRocketParticlesOnPositions()
    {
        MoveRocketParticle(0,8,0.5f);
        MoveRocketParticle(8, 16, 0.75f);
        MoveRocketParticle(16,24,1f);
        
        rocketParticleId = 23;
        rocketParticleCondition = rocketParticleId - 8;
        
        GyrussGameManager.Instance.TogglePlayerSpawned();
        PrepareRocketParticles();
    }

    private void MoveRocketParticle(int index, int condition, float originY)
    {
        int angle = 0;
        Vector3 playerShipPos = playerShip.transform.position;
        Vector3 originPosition = playerShipPos + new Vector3(0, originY, 0);
        
        for (int i = index; i < condition; i++) {
            rocketParticles[i].transform.position = originPosition;
            rocketParticles[i].transform.RotateAround(playerShipPos, Vector3.forward, angle);
            angle += 360 / 8;
        }
    }
    
    private void PrepareRocketParticles()
    {
        if (rocketParticleId < 0) {
            GyrussGameManager.Instance.ShootRocket();
            GyrussGameManager.Instance.TogglePlayerSpawned();
            return;
        }
        
        TurnOnRocketParticle(rocketParticleId, rocketParticleCondition);
        
        rocketParticleId -= 8;
        rocketParticleCondition -= 8;
        
        GyrussGameManager.Instance.SetConditionInTimer("rocketParticleDelay", true);
    }

    private void TurnOnRocketParticle(int particleId, int condition)
    {
        for (int i = particleId; i > condition; i--) {
            rocketParticles[i].SetActive(true);
        }
    }
    
    private void CreateExplosion(Vector3 explosionPosition)
    {
        GameObject explosion = Instantiate(ExplosionPrefab, explosionPool, true);
        explosion.transform.position = explosionPosition;
        explosion.GetComponent<DeathController>().Die();
    }
    
}
