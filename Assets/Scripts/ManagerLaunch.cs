using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerLaunch : MonoBehaviour
{

    public PlayerControlScript Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = (Target.transform.position - transform.position).normalized;
        transform.up = targetDirection;
    }
}
