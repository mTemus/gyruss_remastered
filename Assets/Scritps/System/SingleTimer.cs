using UnityEngine;
using UnityEngine.Events;

public class SingleTimer : MonoBehaviour
{
    [SerializeField] private string timerName;
    [SerializeField] private float timerPeriod;
    [SerializeField] private UnityEvent timerEvent;
    
    private float timer;
    private float initialPeriod;
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

    public void StopTimer()
    {
        condition = false;
        timer = timerPeriod;
    }

    public void SetTimerPeriod(float newPeriod)
    {
        if (initialPeriod == 0) initialPeriod = timerPeriod;
        timerPeriod = newPeriod;
    }

    public void ResetTimerPeriod()
    {
        timerPeriod = initialPeriod;
        initialPeriod = 0;
    }
    
    public string TimerName => timerName;
    
    
}
