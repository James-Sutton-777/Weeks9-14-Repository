using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class MissileControlScript : MonoBehaviour
{

    public PlayerControlScript Target;

    public bool targetValidated;
    public bool targetLost = false;

    float timer;
    public float confusionTime;

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

    public AnimationCurve curve;

    Vector3 targetProjection;
    Vector3 missileVelocity;
    Vector3 seekerDirection;
    Vector3 targetDirection;
    Vector3 targetDirectionActual;
    Vector3 seekerTracking;

    Vector3 targetPosition;
    Vector3 targetVelocity;
    Vector3 targetDirectionAbsolute;

    Quaternion targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        targetDirection = Target.transform.position - transform.position;
        targetValidated = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetDirectionActual = targetPosition - transform.position;
        targetDirectionAbsolute = Target.transform.position - transform.position;
        targetDirection = targetProjection - transform.position;
        distanceToTarget = targetDirection.magnitude;
        MissileSeeker();

        if (targetValidated == true)
        {
            TargetValidation();
        }
        MissilePersuitManeuvering();

        //for debugging
        Debug.DrawLine(transform.position, Target.transform.position);
        Quaternion rotation = Quaternion.Euler(0, 0, seekerAngleThreshold);
        Quaternion rotation2 = Quaternion.Euler(0, 0, (-1 * seekerAngleThreshold));
        Vector3 seekerFovOne = rotation * (targetDirectionActual * 50);
        Vector3 seekerFovTwo = rotation2 * (targetDirectionActual * 50);
        Debug.DrawLine(transform.position, seekerFovOne);
        Debug.DrawLine(transform.position, seekerFovTwo);
        Debug.DrawLine(transform.position, transform.position + seekerTracking);
        angle = Vector3.Angle(seekerTracking, targetDirectionAbsolute);

        

    }

    void MissileSeeker()
    {
        //
        seekerTracking = targetDirectionActual;

        //hit validator
        if(distanceToTarget < hitRange)
        {
            Debug.Log("Hit");
        }

        if (Vector3.Angle(seekerTracking, targetDirectionAbsolute) > seekerAngleThreshold)
        {
            Debug.Log("targetlost");
            targetValidated = false;
            targetLost = true;
        }
    }

    

    void TargetValidation()
    {
        targetPosition = Target.transform.position;
        targetVelocity = Target.velocityOfPlayer;
    }

    public void CounterMeasureEvent()
    {
        StartCoroutine(TargetDeployingCounterMeasures());
    }

    IEnumerator TargetDeployingCounterMeasures()
    {
        if (timer != confusionTime)
        {
            targetValidated = false;
            timer = confusionTime;

            Debug.Log(timer);

            yield return new WaitForSeconds(confusionTime);
        }
            targetValidated = true;
            timer = 0;
    }

    void MissilePersuitManeuvering()
    {
        if (distanceToTarget > thresholdStageTwo)
        {
            projectionDistance = staticProjection;
        }
        else
        {
            projectionDistance = curve.Evaluate(1 - distanceToTarget / thresholdStageTwo) * staticProjection;
        }


        targetProjection = targetPosition + (targetVelocity * projectionDistance);
        targetRotation = Quaternion.LookRotation(Vector3.forward, (targetProjection - transform.position).normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, missileTurnSpeed * Time.deltaTime);
        transform.position += transform.up * speed * Time.deltaTime;
    }

    void MissileEvaded()
    {
        //destroy
        //call manager event function for evaded missile
    }

    void MissileHit()
    {
        //destroy
        //call hit event in manager
    }
}
