using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IndicatorController : MonoBehaviour
{
    //create object refrence to player and assigned missile
    public GameObject threat;
    public GameObject player;
    //declare sprite renderer variable
    public SpriteRenderer indicator;
    //assigns distance at which indicator is offset from player
    public float distanceFromPlayer;
    //vector containing missile direction from player
    Vector3 threatDirection;
    //boolean for when countermeasure event is on
    bool eventOn = false;
    //timer float to count during event for changing of color and timer maximum
    float timer = 0;
    public float timerMax;
    //colors for sprite color change during event
    Color active;
    Color inactive;

    //runs once at start of program
    private void Start()
    {
        //assign color values to color variables
        active = new Color(255, 0, 0, 255);
        inactive = new Color(0, 0, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        //calculate missile direction
        threatDirection = threat.transform.position - player.transform.position;
        //place indicator at distance between player and missile
        transform.position = player.transform.position + threatDirection.normalized * distanceFromPlayer;
        //make indicator point towards missile
        transform.up = threatDirection.normalized;
        //call color change function
        IndicatorColourChange();
    }

    //function starting coroutine to change color of indicator during countermeasure event
    public void MissilesAreDumb()
    {
        //set event bool to active
        eventOn = true;
        Debug.Log("Missiles are Dumb");
        
    }

    //coroutine changes indicator colour
    void IndicatorColourChange()
    {
        //if event is active run statment code
        if(eventOn == true)
        {
            //change color to inactive variable
            indicator.color = inactive;
            //count timer for duration of event
            timer += Time.deltaTime;
            
            Debug.Log("color should change");
        }
        if (timer >= timerMax)
        {
            eventOn = false;
            //change colour back to active variable and end coroutine
            indicator.color = active;
        }
        
    }

    //function to destroy indicator when missile despawns
    public void MissileDead()
    {
        //destroys indicator in 3 seconds
        Destroy(gameObject,3);
        //deactivates indicator spriterender
        indicator.enabled = false;
    }
}
