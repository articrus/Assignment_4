using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This script is used to controll the lich boss
 */
public class LichController : MonoBehaviour
{
    public float flySpeed = 3.0f;
    public float[] moveSpeeds = {50, 5000, 100000 };
    public FOV fov;
    public bool _HasTarget = false;
    public float stopRate = 0.05f;
    public float knockbackResist = 1.0f; //Resists knockback

    //The Boss' positions in the scene
    public List<Transform> dashPoints;
    public List<Transform> hoverPoints;
    public List<Transform> firePoints;
    public Transform outside;
    public float reached = 0.1f;
    //Times and durations
    public float timeElapsed = 0.0f;
    public float turnTime = 6.0f;

    Animator anim;
    Rigidbody2D rb;
    Transform nextPoint;
    LevelLoader switcher;
    public int randomNum = 0;
    int newNum;
    bool starting;

    public bool IsAlive { get { return anim.GetBool(AnimationStrings.isAlive); } }

    public bool CanMove { get { return anim.GetBool(AnimationStrings.canMove); } }

    public bool DashAttack { get{ return anim.GetBool(AnimationStrings.dshAtk); } }

    public bool FlameAttack { get { return anim.GetBool(AnimationStrings.flameAtk); } }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        switcher = GameObject.FindGameObjectWithTag("End").GetComponent<LevelLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        randomNum = 2;
        starting = true;
    }

    // Update is called once per frame
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= turnTime)
        {
            randomNum++;
            if(randomNum > 2) { randomNum = 0; }
            timeElapsed = 0;
            newNum = Random.Range(0, 3);
            switch (randomNum)
            {
                //Dash Attack
                case 0: anim.SetTrigger(AnimationStrings.dashAttack);
                    break;
                //Flames Attack
                case 1: anim.SetTrigger(AnimationStrings.flamesAttack); 
                    break;
                //Hover in place
                case 2: anim.SetTrigger(AnimationStrings.hover); 
                    gameObject.transform.position = hoverPoints[newNum].position; 
                    break;
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (IsAlive)
        {
            if (!CanMove)
            {
                rb.velocity = Vector2.zero;
            }
            else if (DashAttack) //Moving during the dash
            {
                if (starting)
                {
                    Vector2 outsideDash = (outside.position - transform.position).normalized;
                    float outDist = Vector2.Distance(outside.position, transform.position);
                    rb.velocity = outsideDash * 30;
                    if (outDist <= reached) { starting = false; }

                }
                else
                {
                    Vector2 directionTo = (dashPoints[1].position - transform.position).normalized;
                    rb.velocity = directionTo * 30;
                }
            }else if (FlameAttack) //Moving during the fire attack
            {
                if (starting)
                {
                    Vector2 outsideDash = (outside.position - transform.position).normalized;
                    float outDist = Vector2.Distance(outside.position, transform.position);
                    rb.velocity = outsideDash * moveSpeeds[2];
                    if (outDist <= reached) { starting = false; }

                }
                else
                {
                    Vector2 directionTo = (firePoints[1].position - transform.position).normalized;
                    float dist = Vector2.Distance(firePoints[1].position, transform.position);
                    rb.velocity = directionTo * moveSpeeds[1];
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            switcher.LoadNextLvl(switcher.lvlSceneNum);
        }
    }
}