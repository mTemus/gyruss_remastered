using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private Dictionary<string, SingleTimer> timers;
    
    private void Start()
    {
        SetDelegates();
        GetAllTimers();
    }

    private void SetDelegates()
    {
        GyrussEventManager.ConditionSetupInTimerInitiated += SetConditionInTimer;
    }

    private void GetAllTimers()
    {
        timers = new Dictionary<string, SingleTimer>();

        foreach (Transform child in transform) {
            SingleTimer timer = child.GetComponent<SingleTimer>();
            string timerName = timer.TimerName;
            timers[timerName] = timer;
        }
    }

    private void SetConditionInTimer(string timerName, bool condition)
    {
        timers[timerName].SetTimerCondition(condition);
    }
}



