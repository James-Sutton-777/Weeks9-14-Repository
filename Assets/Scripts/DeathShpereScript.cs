using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathShpereScript : MonoBehaviour
{

    public AnimationCurve curve;

    [Range(0, 1)]
    float t;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        transform.localScale = Vector3.one * curve.Evaluate(t);
    }
}
