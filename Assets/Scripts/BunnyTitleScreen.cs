using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnyTitleScreen : MonoBehaviour
{
    [Range(3f, 5f)]
    public float minRandTimer = 3.5f;

    [Range(5f, 8f)]
    public float maxRandTimer = 6.5f;

    private Animator animator;
    private float timer = 0f;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = Random.Range(minRandTimer, maxRandTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0f)
        {
            animator.SetTrigger("Scare");
            timer = Random.Range(minRandTimer, maxRandTimer);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
