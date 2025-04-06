using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MissileControlScript : MonoBehaviour
{

    public PlayerControlScript Target;

    public bool targetValidated = true;

    public float predictionTime;
    public float staticProjection;
    public float speed;
    public float projectionDistance;
    public float distanceToTarget;
    public float thresholdStageTwo;
    public float missileTurnSpeed;
    public float seekerAngleThreshold;

    public AnimationCurve curve;

    Vector3 targetProjection;
    Vector3 missileVelocity;
    Vector3 seekerDirection;
    Vector3 targetDirection;
    Vector3 seekerTracking;

    Vector3 targetPosition;
    Vector3 targetVelocity;

    Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        targetDirection = Target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //seekerDirection = Target.transform.position - transform.position;
        //transform.up = seekerDirection.normalized;

        TargetValidation();
        MissileSeeker();

        targetDirection = targetProjection - transform.position;
        distanceToTarget = targetDirection.magnitude;


        if( distanceToTarget > thresholdStageTwo)
        {
            projectionDistance = staticProjection;
        }
        else
        {
            projectionDistance = curve.Evaluate(1 - distanceToTarget / thresholdStageTwo) * staticProjection;
        }
        

        targetProjection = targetPosition + (targetVelocity * projectionDistance);
        if(targetValidated == true)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, (targetProjection - transform.position).normalized);
        }
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, missileTurnSpeed * Time.deltaTime);
        transform.position += transform.up * speed * Time.deltaTime;
        
    }

    void MissileSeeker()
    {
        seekerTracking = targetDirection.normalized;

        if (Vector3.Angle(seekerTracking, targetPosition) > seekerAngleThreshold)
        {
            Debug.Log("targetlost");
            targetValidated = false;
        }
    }

    void TargetValidation()
    {
        targetPosition = Target.transform.position;
        targetVelocity = Target.velocityOfPlayer;
    }
}
