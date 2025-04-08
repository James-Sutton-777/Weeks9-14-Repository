using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerLaunch : MonoBehaviour
{

    public PlayerControlScript Target;
    public GameObject Player;

    public List<GameObject> missiles;
    public List<GameObject> indicators;

    public GameObject missilePrefab;
    public GameObject indicatorPrefab;

    public float maxMissileCount;
    public float timer = 0;

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
    }

    void MissileAndIndicatorSpawner()
    {
        timer += 1 * Time.deltaTime;
        if (timer > 5)
        {
            GameObject newMissile = Instantiate(missilePrefab, transform.position, transform.rotation);
            GameObject newIndicator = Instantiate(indicatorPrefab, Player.transform.position, Quaternion.identity);

            timer = 0;
        }
    }
}
