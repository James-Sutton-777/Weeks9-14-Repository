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

    public AnimationCurve distanceToTarget;

    Vector3 missileDirection;
    Vector3 missileVelocity;
    Vector3 seekerDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //seekerDirection = Target.transform.position - transform.position;
        //transform.up = seekerDirection.normalized;
        //predictionModifier = 

        missileDirection = Target.transform.position + Target.velocityOfPlayer * predictionModifier;
        transform.up = missileDirection.normalized;
        transform.position += transform.up * speed * Time.deltaTime;
        
    }
}
