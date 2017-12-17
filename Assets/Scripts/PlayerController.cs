using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private Rigidbody2D rb;
    private Vector3 direction;

    public Transform headBone;      //the bone of the head
    public Transform bodyBone;      //the bone for the body
    public Transform[] earsIK;      //the IK controllers for the ears


    //player sounds
    //public AudioClip[] steps;       //holds stepping sound effects
    public float stepSpeed = 1f;
    private float stepCounter;
    private AudioSource stepAudioSrc;

    private AudioSource thudSrc;

    private float movementSpeed;

    //Animator
    private Animator animator;

    //IK ears stuff
    const float STARTING_X = -0.005f;
    const float EAR_TRAVEL_DIST = 0.1f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        thudSrc = audioSources[0];
        stepAudioSrc = audioSources[1];
    }

    // Fixed Update for physics
    void FixedUpdate()
    {
        //direction of movement is combination of horizontal input and vertical input
        direction = (Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up).normalized;

        //add acceleartion
        rb.AddForce(direction * speed * Time.fixedDeltaTime);
    }

    // Update visual effects
    private void Update()
    {
        UpdateBodyHeadOrientation();
        AdjustEars();

        movementSpeed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
        UpdateAnimation(movementSpeed);
        UpdateWalkSound(movementSpeed);
    }

    //Adjust the orietnation of the head & body orientation
    void UpdateBodyHeadOrientation()
    {
        if (bodyBone && rb)
        {
            bodyBone.right = Vector3.Lerp(bodyBone.right, rb.velocity, Time.deltaTime * 15f);

            if (headBone)
                headBone.right = Vector3.Lerp(headBone.right, (bodyBone.right + direction) / 2f, Time.deltaTime * 5f);
        }
    }

    //Adjusts the IK controllers that are controlling the ears
    void AdjustEars()
    {
        foreach (Transform t in earsIK)
        {
            Vector3 locPos = t.localPosition;
            locPos.x = STARTING_X - rb.velocity.sqrMagnitude * EAR_TRAVEL_DIST;
            t.localPosition = locPos;
        }
    }

    //Plays the walk sounds
    void UpdateWalkSound(float speed)
    {
        stepCounter += speed * Time.deltaTime;
        if(stepCounter > stepSpeed)
        {
            stepAudioSrc.pitch = Random.Range(0.75f, 1.25f);
            stepAudioSrc.Play();
            stepCounter = 0;
        }
    }

    void UpdateAnimation(float speed)
    {
        if (speed < 0.5f)
        {
            animator.SetBool("IsMoving", false);
        } 
        else
        {
            animator.SetBool("IsMoving", true);
            animator.speed = speed / 8f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (movementSpeed < 7.5f) return;

            thudSrc.pitch = Random.Range(0.85f, 1.15f);
            thudSrc.Play();
        }
    }
}
