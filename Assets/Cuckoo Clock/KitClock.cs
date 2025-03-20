using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public float timeAnHourTakes = 5;

    public float t;
    public int hour = 0;

    public UnityEvent OnTheHour;

    void Start()
    {
        StartCoroutine(MoveClockHandsOneHour());
    }
    //coroutine moves hour and minute hand the equivalent of 1 hour of movement
    IEnumerator MoveClockHandsOneHour()
    {
        while(t < timeAnHourTakes)
        {
            t += Time.deltaTime;

            minuteHand.Rotate(0, 0, -(360 / timeAnHourTakes) * Time.deltaTime);
            hourHand.Rotate(0, 0, -(30 / timeAnHourTakes) * Time.deltaTime);
            yield return null;
        }
        OnTheHour.Invoke();
    }
}
