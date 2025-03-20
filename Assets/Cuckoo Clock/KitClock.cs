using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public float timeAnHourTakes = 5;

    Coroutine clockIsRunning;
    Coroutine handsMovingOneHour;

    public float t;
    public int hour = 0;

    public UnityEvent OnTheHour;
    private void Start()
    {
        clockIsRunning = StartCoroutine(ClockCompleteLocamotion());
    }

    IEnumerator ClockCompleteLocamotion()
    {
        //Using a single line of code multiple times indicates potential to be turned into a loop
        //Using a the true == true allows the loop to run perpetually, however the yield allows the game to retain control of what code is running
        while(true)
        {
            yield return handsMovingOneHour = StartCoroutine(MoveClockHandsOneHour());
        }
    }
    //coroutine moves hour and minute hand the equivalent of 1 hour of movement
    IEnumerator MoveClockHandsOneHour()
    {
        t = 0;
        while(t < timeAnHourTakes)
        {
            t += Time.deltaTime;

            minuteHand.Rotate(0, 0, -(360 / timeAnHourTakes) * Time.deltaTime);
            hourHand.Rotate(0, 0, -(30 / timeAnHourTakes) * Time.deltaTime);

            //yield instruct it to return to line 33 next frame which being the end of the loop causes the program to check to the loop.
            yield return null;
        }
        OnTheHour.Invoke();
    }

    public void StopClock()
    {
        StopCoroutine(clockIsRunning);
        StopCoroutine(handsMovingOneHour);
    }
}
