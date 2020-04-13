﻿using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float speed = 4;
    private Vector3 playerPosition;
    private Vector3 exitPosition;

    private bool move;

    private void Update()
    {
        if (!move) return;
        
        transform.position = Vector3.MoveTowards(transform.position, exitPosition, speed * Time.deltaTime);

        if (transform.position == exitPosition) Destroy(transform.gameObject); 
    }
    
    public void CalculateExitPosition()
    {
        exitPosition = new Vector3(playerPosition.x * 2, playerPosition.y * 2, 0);
        move = true;
    }
    
    public Vector3 PlayerPosition
    {
        get => playerPosition;
        set => playerPosition = value;
    }
}