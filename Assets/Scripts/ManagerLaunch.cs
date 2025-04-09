using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ManagerLaunch : MonoBehaviour
{

    public PlayerControlScript Target;
    public GameObject Player;

    public List<GameObject> missiles;
    public List<GameObject> indicators;

    public GameObject missilePrefab;
    public GameObject indicatorPrefab;

    //Countermeasures event
    public UnityEvent counterMeasures;


    public float maxMissileCount;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        missiles = new List<GameObject>();
        indicators = new List<GameObject>();
        MissileAndIndicatorSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = (Target.transform.position - transform.position).normalized;
        transform.up = targetDirection;

        
    }

    void MissileAndIndicatorSpawner()
    {
            GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
            MissileControlScript missileScript = newMissile.GetComponent<MissileControlScript>();
            missileScript.Target = Target;
            counterMeasures.AddListener(missileScript.TargetDeployingCounterMeasures);
            missiles.Add(newMissile);
            GameObject newIndicator = Instantiate(indicatorPrefab, Player.transform);
            indicators.Add(newIndicator);
            IndicatorController indicatorScript = newIndicator.GetComponent<IndicatorController>();
            indicatorScript.threat = newMissile;
            indicatorScript.player = Player;
    }

    void DeployCounterMeasures()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            counterMeasures.Invoke();
        }
    }

}
