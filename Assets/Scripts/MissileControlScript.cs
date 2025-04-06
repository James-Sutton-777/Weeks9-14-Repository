using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MissileControlScript : MonoBehaviour
{

    public PlayerControlScript Target;

    public float predictionTime;
    public float predictionModifier;
    public float speed;
    public float time;
    public float projection;
    public float launchDistance;
    public float currentDistance;
    public float distanceToTarget;

    public AnimationCurve curve;

    Vector3 missileDirection;
    Vector3 missileVelocity;
    Vector3 seekerDirection;
    Vector3 targetDirection;
    // Start is called before the first frame update
    void Start()
    {
        targetDirection = Target.transform.position - transform.position;
        launchDistance = targetDirection.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        //seekerDirection = Target.transform.position - transform.position;
        //transform.up = seekerDirection.normalized;

        time += 1 * Time.deltaTime;

        targetDirection = Target.transform.position - transform.position;
        currentDistance = targetDirection.magnitude;


        distanceToTarget = launchDistance - currentDistance;
        predictionModifier = Mathf.Lerp(0, 1, distanceToTarget);
        
        projection = curve.Evaluate(predictionModifier);
        

        missileDirection = Target.transform.position + (Target.velocityOfPlayer);
        transform.up = missileDirection.normalized;
        transform.position += transform.up * speed * Time.deltaTime;
        
    }
}
