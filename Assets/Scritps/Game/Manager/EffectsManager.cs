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
    
    private List<GameObject> readyReviveParticles;
    private Vector3 playerShipPosition;

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

    private void CreateExplosion(Vector3 explosionPosition)
    {
        GameObject explosion = Instantiate(ExplosionPrefab, explosionPool, true);
        explosion.transform.position = explosionPosition;
        explosion.GetComponent<DeathController>().Die();
    }
    
}
