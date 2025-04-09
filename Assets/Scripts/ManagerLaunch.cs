using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
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

    public float score = 0;
    public bool TargetActive = true;

    public float maxMissileCount;
    public float timer = 0;
    public float timerMax;
    public bool coolDownActive = false;
    public float cooldownTimer = 10;
    public float cooldownMax;

    // Start is called before the first frame update
    void Start()
    {
        missiles = new List<GameObject>();
        indicators = new List<GameObject>();    
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = (Target.transform.position - transform.position).normalized;
        transform.up = targetDirection;

        if(cooldownTimer >= cooldownMax)
        {
            DeployCounterMeasures();
        }
        if(coolDownActive == true)
        {
            CoolDownOn();
        }

        if((missiles.Count < maxMissileCount) && TargetActive == true)
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                MissileAndIndicatorSpawner();
                timer = 0;
            }
        }

        
    }

    void MissileAndIndicatorSpawner()
    {
        GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
            MissileControlScript missileScript = newMissile.GetComponent<MissileControlScript>();
            missileScript.Target = Target;
        missileScript.Manager = this;
        counterMeasures.AddListener(missileScript.CounterMeasureEvent);
            GameObject newIndicator = Instantiate(indicatorPrefab, Player.transform);
            IndicatorController indicatorScript = newIndicator.GetComponent<IndicatorController>();
            indicatorScript.threat = newMissile;
            indicatorScript.player = Player;
        missileScript.MyIndicator = indicatorScript;
        indicators.Add(newIndicator);
        missiles.Add(newMissile);
    }

    void DeployCounterMeasures()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cooldownTimer = 0;
            coolDownActive = true;
            counterMeasures.Invoke();
            Debug.Log("target Evading");
        }
    }

    void CoolDownOn()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer >= cooldownMax)
        {
            coolDownActive = false;
        }
    }

   public void MissilesEvaded()
    {
        score += 1;
        Debug.Log(score);
    }

}
