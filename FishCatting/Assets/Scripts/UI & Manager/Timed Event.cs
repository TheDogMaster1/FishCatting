using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    public float time;
    public UnityEvent waitEvent;
    private float timeToStart;
    private void OnEnable()
    {
        timeToStart = time + Time.timeSinceLevelLoad;
    }
    void Update()
    {
        if(Time.timeSinceLevelLoad > timeToStart)
        {
            waitEvent.Invoke();
            this.enabled = false;
        }
    }
}
