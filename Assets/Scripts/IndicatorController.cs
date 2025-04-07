using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IndicatorController : MonoBehaviour
{
    public GameObject threat;
    public GameObject player;

    Vector3 threatDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        threatDirection = threat.transform.position - player.transform.position;
        transform.up = threatDirection;

        transform.localPosition = threatDirection.normalized * 5;
    }
}
