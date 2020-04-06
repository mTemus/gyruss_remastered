using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleTimer : MonoBehaviour
{
    [SerializeField] private string timerName;
    [SerializeField] private float timerPeriod;
    [SerializeField] private UnityEvent timerEvent;
    
    private float timer;
    private bool condition;
    
    void Start()
    {
        timer = timerPeriod;
    }

    void Update()
    {
        if (!condition) return;
        timer -= Time.deltaTime;

        if (!(timer <= 0)) return;
        
        condition = false;
        timerEvent.Invoke();
        timer = timerPeriod;
    }

    public void SetTimerCondition(bool condition)
    {
        this.condition = condition;
    }
    
    public string TimerName => timerName;
    
    
}
