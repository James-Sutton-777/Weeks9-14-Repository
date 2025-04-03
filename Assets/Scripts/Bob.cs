using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{

    public CinemachineImpulseSource GiantShake;

    SpriteRenderer sr;
    Animator animator;
    AudioSource audio;
    public List<AudioClip> clips;
    public float speed = 2;
    public bool canRun = true;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");

        sr.flipX = (direction < 0);

        animator.SetFloat("movement", Mathf.Abs(direction));

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            canRun = false;
        }


        if (canRun == true)
        {
            transform.position += transform.right * direction * speed * Time.deltaTime;
        }

    }

    public void AttackHasFinished()
    {
        Debug.Log("The attack animation has finished");
        canRun = true;
    }

    public void FootstepsSound()
    {
        audio.clip = clips[(int)Random.Range(0, 10)];
        audio.Play();

        GiantShake.GenerateImpulse();
    }
}
