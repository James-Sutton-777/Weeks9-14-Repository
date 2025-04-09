using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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
    public float dragCoefficient;

    //Vectors to control movement
    public Vector3 velocityOfPlayer;
    Vector3 directionOfPlayer;
    Vector3 dragOnPlayer;

    //Countermeasures event
    public UnityEvent counterMeasures;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AircraftControls();
    }

    void AircraftControls()
    {
        engineOutput = Mathf.Clamp(engineOutput, 0, 1);
        thrust = enginePower * engineOutput;

        directionOfPlayer = transform.up;

        //turning calculations
        transform.Rotate(Vector3.forward * ((turnSpeed * turnSpeedRatio) * Input.GetAxisRaw("Horizontal")) * Time.deltaTime);

        dragOnPlayer = -1 * (dragCoefficient * area * (speed * speed) * velocityOfPlayer.normalized);

        velocityOfPlayer += (dragOnPlayer + (transform.up * thrust)) * Time.deltaTime;
        transform.position += velocityOfPlayer * Time.deltaTime;

        speed = velocityOfPlayer.magnitude;

        if (Input.GetAxisRaw("Vertical") == 1)
        {
            engineOutput += outputForce * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Vertical") == -1)
        {
            engineOutput -= outputForce * Time.deltaTime;
        }
    }

    void DeployCounterMeasures()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            counterMeasures.Invoke();
        }
    }
}
