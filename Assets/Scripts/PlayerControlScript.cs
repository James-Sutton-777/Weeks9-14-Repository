using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControlScript : MonoBehaviour
{
    //Object to hold sphere used for death animation;
    public GameObject DeathSphere;

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
    //Velocity is public so that missile can calculate trajectory
    public Vector3 velocityOfPlayer;
    Vector3 directionOfPlayer;
    Vector3 dragOnPlayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //call function to control player sprite
        AircraftControls();
    }

    void AircraftControls()
    {
        //Determines amount of thrust being generated
        engineOutput = Mathf.Clamp(engineOutput, 0, 1);
        thrust = enginePower * engineOutput;

        //Part of old system used to modify area based on angle difference between velocity and direction player is facing
        directionOfPlayer = transform.up;

        //turning calculations
        transform.Rotate(Vector3.forward * ((turnSpeed * turnSpeedRatio) * Input.GetAxisRaw("Horizontal")) * Time.deltaTime);

        //calculate drag created by player to impact top speed and acceleration
        dragOnPlayer = -1 * (dragCoefficient * area * (speed * speed) * velocityOfPlayer.normalized);

        //callculate velocity than add it to position to update player position
        velocityOfPlayer += (dragOnPlayer + (transform.up * thrust)) * Time.deltaTime;
        transform.position += velocityOfPlayer * Time.deltaTime;

        //use magnitude of velocity to generate speed variable for drag calculation
        speed = velocityOfPlayer.magnitude;

        //if statements dedecting keyboard inputs coresponding with engine output generating a percentage of thrust
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            engineOutput += outputForce * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Vertical") == -1)
        {
            engineOutput -= outputForce * Time.deltaTime;
        }
    }

    //function called by missile to initiate the death animation
    public void HitByMissile()
    {
        //player sprite is turn off
        SpriteRenderer PlayerModel = GetComponent<SpriteRenderer>();
        PlayerModel.enabled = false;
        //explosion is spawn on top of player position at time of impact;
        GameObject explosion = Instantiate(DeathSphere, transform.position, Quaternion.identity);
    }
}
