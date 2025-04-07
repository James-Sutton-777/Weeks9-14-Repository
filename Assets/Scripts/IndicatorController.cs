using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IndicatorController : MonoBehaviour
{
    public GameObject threat;
    public GameObject player;

    public float distanceFromPlayer;

    Vector3 threatDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        threatDirection = threat.transform.position - player.transform.position;
        transform.position = player.transform.position + threatDirection.normalized * distanceFromPlayer;
        transform.up = threatDirection.normalized;
    }
}
