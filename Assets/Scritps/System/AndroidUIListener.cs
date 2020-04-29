using UnityEngine;
using UnityEngine.Events;

public class AndroidUIListener : MonoBehaviour
{
    [SerializeField] private FixedButton myButton = null;
    [SerializeField] private UnityEvent myEvent = null;
    
    void Update()
    {
        if (myButton.Pressed) { myEvent?.Invoke(); }
    }
}
