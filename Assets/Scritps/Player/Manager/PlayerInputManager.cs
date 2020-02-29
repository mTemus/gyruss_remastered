using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private Transform mapCenterPoint;
    [SerializeField] private GameObject playerShip;

    private float speed = 100f;
    
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateShip(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateShip(Vector3.back);
        }
    }

    private void RotateShip(Vector3 rotateAxis)
    {
        playerShip.transform.RotateAround(Vector3.zero,rotateAxis, Time.deltaTime * speed);
    }
    
    
    
}
