using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

public class EventDemo : MonoBehaviour
{
    public RectTransform banana;

    public UnityEvent OnTimerHasFinished;

    public float timerLength = 3;
    public float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerLength)
        {
            timer = 0;
            OnTimerHasFinished.Invoke();
        }
    }

    public void MouseJustEnteredImage()
    {
        Debug.Log("Mouse just entered me!");
        banana.localScale = Vector3.one * 1.2f;
    }

    public void MouseJustExitedImage()
    {
        Debug.Log("Mouse just exited me!");
        banana.localScale = Vector3.one;
    }
}
