using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class ManagerLaunch : MonoBehaviour
{
    //aquire player game object information and script component
    public PlayerControlScript Target;
    public GameObject Player;

    //create list for storing missiles and indicators
    public List<GameObject> missiles;
    public List<GameObject> indicators;

    //create objects to contain missile and indicator prefab
    public GameObject missilePrefab;
    public GameObject indicatorPrefab;

    //Countermeasures event
    public UnityEvent counterMeasures;

    //float contains current score
    public float score = 0;
    //boolean enables launch of missiles
    public bool TargetActive = true;

    //variable determines maximum permited amount of active missiles
    public float maxMissileCount;
    //Timer for interval of missile launches
    public float timer = 0;
    public float timerMax;

    //variables controlling cooldown of player countermeasures ability
    public bool coolDownActive = false;
    public float cooldownTimer = 10;
    public float cooldownMax;

    // Start is called before the first frame update
    void Start()
    {
        //declare lists for missiles and indicators
        missiles = new List<GameObject>();
        indicators = new List<GameObject>();    
        
    }

    // Update is called once per frame
    void Update()
    {
        //have the missile launcher face the player at all times
        Vector3 targetDirection = (Target.transform.position - transform.position).normalized;
        transform.up = targetDirection;

        //if statement controlling if player can use countermeasures
        if(cooldownTimer >= cooldownMax)
        {
            DeployCounterMeasures();
        }

        //if statement activates countermeasures cooldown
        if(coolDownActive == true)
        {
            CoolDownOn();
        }

        //if statement to create new missiles if acitve missiles are bellow maximum permitted amount and player is still active
        if((missiles.Count < maxMissileCount) && TargetActive == true)
        {
            //timer for missile launch delay
            timer += Time.deltaTime;
            //if statement controlling intervals of missile launches
            if (timer > timerMax)
            {
                //call function to create new missile
                MissileAndIndicatorSpawner();
                //reset timer
                timer = 0;
            }
        }

        
    }

    //function spawns missiles, hands over needed refrences and spawns coresponding indicator
    void MissileAndIndicatorSpawner()
    {
        //spawn new missile
        GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
        //aquire missile script component
            MissileControlScript missileScript = newMissile.GetComponent<MissileControlScript>();
        //assign missile target
            missileScript.Target = Target;
        //assign missile manager
        missileScript.Manager = this;
        //subscribe new listner to countemeasure event
        counterMeasures.AddListener(missileScript.CounterMeasureEvent);
        //spawn indicator
            GameObject newIndicator = Instantiate(indicatorPrefab, Player.transform);
        //aquire indicator script component
            IndicatorController indicatorScript = newIndicator.GetComponent<IndicatorController>();
        //assign indicator to corresponding missile
            indicatorScript.threat = newMissile;
        //assing indicator player refrence
            indicatorScript.player = Player;
        //assign missile to corresponding missile
        missileScript.MyIndicator = indicatorScript;
        //place both missile and its indicator in list
        indicators.Add(newIndicator);
        missiles.Add(newMissile);
    }

    //detects player input to activate countermeasures
    void DeployCounterMeasures()
    {
        //when click on left mouse button activate countermeasures event
        if (Input.GetMouseButtonDown(0))
        {
            //reset cooldown timer
            cooldownTimer = 0;
            //block reactivation of event
            coolDownActive = true;
            //activate event
            counterMeasures.Invoke();
            Debug.Log("target Evading");
        }
    }

    //controls amount of time between event activations
    void CoolDownOn()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer >= cooldownMax)
        {
            //enables event activation
            coolDownActive = false;
        }
    }

    //scores points when missiles are evaded
   public void MissilesEvaded()
    {
        score += 1;
        Debug.Log(score);
    }

}
