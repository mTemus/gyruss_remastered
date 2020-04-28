using UnityEngine;
using UnityEngine.Events;

public class SingleTimer : MonoBehaviour
{
    [SerializeField] private string timerName = null;
    [SerializeField] private float timerPeriod = 0;
    [SerializeField] private UnityEvent timerEvent = null;
    
    private float timer;
    private float initialPeriod;
    private bool condition;
    
    void Start()
    {
        timer = timerPeriod;
        initialPeriod = timerPeriod;
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
        timerPeriod = newPeriod;
    }

    public void ResetTimerPeriod()
    {
        timerPeriod = initialPeriod;
    }
    
    public string TimerName => timerName;
    
    
}
