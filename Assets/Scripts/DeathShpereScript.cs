using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathShpereScript : MonoBehaviour
{
    //curve to animate sphere
    public AnimationCurve curve;
    //ranged float to be evaluated in curve
    [Range(0, 1)]
    float t;
    // Start is called before the first frame update
    void Start()
    {
        //destroy sphere in 5 seconds
        Destroy(gameObject, 5);
        //set t to 0
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //increase t by 1 per second
        t += Time.deltaTime;
        //transform sphere scale for pulse effect
        transform.localScale = Vector3.one * curve.Evaluate(t);
    }
}
