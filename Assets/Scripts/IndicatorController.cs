using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IndicatorController : MonoBehaviour
{
    //create object refrence to player and assigned missile
    public GameObject threat;
    public GameObject player;
    //assigns distance at which indicator is offset from player
    public float distanceFromPlayer;
    //vector containing missile direction from player
    Vector3 threatDirection;

    // Update is called once per frame
    void Update()
    {
        //calculate missile direction
        threatDirection = threat.transform.position - player.transform.position;
        //place indicator at distance between player and missile
        transform.position = player.transform.position + threatDirection.normalized * distanceFromPlayer;
        //make indicator point towards missile
        transform.up = threatDirection.normalized;
    }

    //function to destroy indicator when missile despawns
    public void MissileDead()
    {
        //destroys indicator in 3 seconds
        Destroy(gameObject,3);
        //deactivates indicator spriterender
        SpriteRenderer indicator = GetComponent<SpriteRenderer>();
        indicator.enabled = false;
    }
}
