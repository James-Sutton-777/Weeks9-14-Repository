using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDemo : MonoBehaviour
{
    public RectTransform banana;
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
