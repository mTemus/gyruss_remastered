using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private GameObject playerShip;
    [SerializeField] private GameObject[] reviveParticles;
    
    private List<GameObject> readyReviveParticles;
    private Vector3 playerShipPosition;
    private static readonly int Entered = Animator.StringToHash("entered");

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
    }
    
    
    
    private void SetReviveParticlesOnPositions()
    {
        Vector3 originPosition = playerShipPosition + new Vector3(0, 2, 0);
        int angle = 0;

        foreach (GameObject reviveParticle in reviveParticles) {
            reviveParticle.transform.position = originPosition;
            reviveParticle.transform.RotateAround(playerShipPosition, Vector3.forward, angle);

            angle += 360 / reviveParticles.Length;
            Debug.Log(angle);
        }
    }

    private void SetPlayerShipPosition(Vector3 playerShipPosition)
    {
        this.playerShipPosition = playerShipPosition;
        SetReviveParticlesOnPositions();
    }

    private void RegisterReviveParticle(GameObject reviveParticle)
    {
        if (readyReviveParticles.Contains(reviveParticle)) return;
        
        readyReviveParticles.Add(reviveParticle);

        if (readyReviveParticles.Count == 8) {
            SpawnPlayerShip();
            
            foreach (GameObject particle in reviveParticles) {
                particle.SetActive(false);
            }
            
            SetReviveParticlesOnPositions();
            
            readyReviveParticles = new List<GameObject>();
        }
    }

    private void SpawnPlayerShip()
    {
        playerShip.transform.position = playerShipPosition;
        playerShip.SetActive(true);
        playerShip.transform.GetChild(0).GetComponent<Animator>().SetBool(Entered, true);
    }

    private void PrepareReviveParticles()
    {
        foreach (GameObject reviveParticle in reviveParticles) {
            reviveParticle.SetActive(true);
            reviveParticle.GetComponent<ReviveParticleController>().PrepareMovement(playerShipPosition);
        }
    }
    
}
