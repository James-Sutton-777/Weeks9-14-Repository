using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class MissileControlScript : MonoBehaviour
{
    //Script variables for missile to communicate with the spawner, its indicator, and its target
    public PlayerControlScript Target;
    public ManagerLaunch Manager;
    public IndicatorController MyIndicator;

    //boolean dictating if target position is to be updated and tracked
    public bool targetValidated;

    //variables controlling confusion mechanic when countermeasures are deployed
    float timer;
    public float confusionTime;

    //variable controlling calculations for missile guidance and movement
    public float predictionTime;
    public float staticProjection;
    public float speed;
    public float projectionDistance;
    public float distanceToTarget;
    public float thresholdStageTwo;
    public float missileTurnSpeed;
    public float seekerAngleThreshold;
    public float hitRange;
    public float cruiseSpeed;
    public float angle;

    //animation curve used to control missile behavior in stage two of tracking
    public AnimationCurve curve;
    //variable containing vectors pertaining to missile targeting and movement
    Vector3 targetProjection;
    Vector3 missileVelocity;
    Vector3 seekerDirection;
    Vector3 targetDirection;
    Vector3 targetDirectionActual;
    Vector3 seekerTracking;

   //variables containing vectors of targets true values
    Vector3 targetPosition;
    Vector3 targetVelocity;
    Vector3 targetDirectionAbsolute;

    Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        //initial direction used for target projection starts at target origin
        targetDirection = Target.transform.position - transform.position;
        //Target is valid at launch
        targetValidated = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Targets actual bearing from missile using target validation data
        targetDirectionActual = targetPosition - transform.position;
        //Targets actual bearing from missile using targets actual coordinates
        targetDirectionAbsolute = Target.transform.position - transform.position;
        //Bearing of targets projection from missile
        targetDirection = targetProjection - transform.position;
        //Distance between missile and projected target
        distanceToTarget = targetDirection.magnitude;
        //call function that controls seeker detection
        MissileSeeker();

        //if statement allows update of target validation data if target is being tracked
        if (targetValidated == true)
        {
            TargetValidation();
        }
        //missile movement function
        MissilePersuitManeuvering();

        
    }

    //function controls missile seeker or field of vision and tracking of target
    void MissileSeeker()
    {
        //missile field of vision is locked to actual position of target based on validated data
        seekerTracking = targetDirectionActual;

        //hit validator checking if target is within kill range
        if(distanceToTarget < hitRange)
        {
            //call missile hit function to initiate behavior after succesful intercept
            MissileHit();
            Debug.Log("Hit");
        }

        //check if targets actual position is within missile field of view
        if (Vector3.Angle(seekerTracking, targetDirectionAbsolute) > seekerAngleThreshold)
        {
            Debug.Log("targetlost");
            //call missile missed function initiates unsuccesful intercept behavior
            MissileEvaded();
        }
    }

    
    //function unpdates snapshot of target location and velocity
    void TargetValidation()
    {
        targetPosition = Target.transform.position;
        targetVelocity = Target.velocityOfPlayer;
    }

    //public function starts coroutine which temporarily stop update of target data
    public void CounterMeasureEvent()
    {
        StartCoroutine(TargetDeployingCounterMeasures());
    }

    //coroutine disables target data update for set amount of time then enables it once again and sets time back to 0
    IEnumerator TargetDeployingCounterMeasures()
    {
        //first run of coroutine will be just the if statement
        if (timer != confusionTime)
        {
            targetValidated = false;
            timer = confusionTime;

            Debug.Log(timer);

            //wait set time before running coroutine again
            yield return new WaitForSeconds(confusionTime);
        }
        //second run of corountine resets values and ends the coroutine
            targetValidated = true;
            timer = 0;
    }

    //Missile movement functions
    void MissilePersuitManeuvering()
    {
        //if statment using static projection of missile at long distance and more accurate projection at short distance
        if (distanceToTarget > thresholdStageTwo)
        {
            projectionDistance = staticProjection;
        }
        else
        {
            //curve evaluates distance to target in terminal phase causing rapid intercept of target
            projectionDistance = curve.Evaluate(1 - distanceToTarget / thresholdStageTwo) * staticProjection;
        }
        //create target projection vector
        targetProjection = targetPosition + (targetVelocity * projectionDistance);
        
        
        //The idea to use quanternion functions in lines 159 and 161 was proposed by Max Gelina(in programming 2 at sheridan college 2025)
        //creates rotation between projected target and missile
        targetRotation = Quaternion.LookRotation(Vector3.forward, (targetProjection - transform.position).normalized);
        //rotates missile missile towards new rotation at determined speed every second
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, missileTurnSpeed * Time.deltaTime);
        
        
        //applies forward momentum to missile in current heading
        transform.position += transform.up * speed * Time.deltaTime;
    }

    //function that executes behavior upon missile missing target
    void MissileEvaded()
    {
        //Destroy missile in 3 seconds
        Destroy(gameObject, 3);
        //Call function in manager for when missile has been evaded
        Manager.MissilesEvaded();
        //call function to destroy assigned indicator
        MyIndicator.MissileDead();
        //turn off sprite renderer
        SpriteRenderer missileSprite = GetComponent<SpriteRenderer>();
        missileSprite.enabled = false;
    }
    //function that executes behavior upon hitting target
    void MissileHit()
    {
        //call function to destroy assigned indicator
        MyIndicator.MissileDead();
        //Call function to initiate player death sequence
        Target.HitByMissile();
        //destroy missile in 3 seconds
        Destroy(gameObject, 3);
        //Disable manager boolean allowing the continuation of missile launches
        Manager.TargetActive = false;
    }
}
