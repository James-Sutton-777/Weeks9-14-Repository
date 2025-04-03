using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    //Flight model Values

    //Engine variables
    public float enginePower = 150;
    public float engineOutput;
    public float outputForce = 0.33f;
    public float thrust;

    //Variable to store speed
    public float speed;

    //Turning and aerodynamics varibales
    public float idealTurnSpeed;
    public float minTurnSpeed;
    public float maxTurnSpeed;
    public float turnSpeed;
    public float turnSpeedRatio;
    public float minTurnSpeedRatio;
    public float area;

    //Vectors to control movement


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
