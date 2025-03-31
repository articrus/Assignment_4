using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 17-11-2024
 * This script is used when creating general grunt type enemies and minor bosses that attack and walk around
 */
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class AttackGrunt : MonoBehaviour
{
    //Grunt Variables
    public float walkSpeed = 3.0f;
    public FOV fov;
    public bool _HasTarget = false;
    public float stopRate = 0.05f;
    public float knockbackResist = 1.0f; //Some grunts resist knockback better than others

    //Object Components
    Rigidbody2D rigidBod;
    TouchingDirections touchingDirections;
    Animator anim;
    Damageable damageable;

    //For moving left and right
    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection = WalkableDirection.Right;
    private Vector2 walkDirectionVector;

    //This code is used to change the value of the walking direction enum
    //The enemy flips by inverting the localscale (*-1)
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                //Flip the direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right) { walkDirectionVector = Vector2.right; }
                else if (value == WalkableDirection.Left) { walkDirectionVector = Vector2.left; }
            }
            _walkDirection = value;
        }
    }

    //If a target (player) enters the FOV, attack it (for attack to trigger, hasTarget must be true
    public bool HasTarget
    {
        get { return _HasTarget; }
        private set
        {
            _HasTarget = value;
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove { get { return anim.GetBool(AnimationStrings.canMove); } }

    //Used for when applying knockback to grunts from player attacks
    public bool LockVelocity
    {
        get { return anim.GetBool(AnimationStrings.lockVelocity); }
        set { anim.SetBool(AnimationStrings.lockVelocity, value); }
    }

    //To prevent enemies from attacking a player 100 times in a row, add a short pause after every attack
    public float AttackCooldown
    {
        get { return anim.GetFloat(AnimationStrings.attackPause); }
        private set { anim.SetFloat(AnimationStrings.attackPause, Mathf.Max(value, 0)); }
        //If less than zero set to 0 
    }

    private void Awake()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    //Flip the direction of the enemy when the Walkable direction enum is equal to left or right
    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Left) { WalkDirection = WalkableDirection.Right; }
        else if (WalkDirection == WalkableDirection.Right) { WalkDirection = WalkableDirection.Left; }
        else { Debug.Log("ERROR: ILLEGAL WALKABLE DIRECTION VALUE"); }
    }

    //Move the grunt or flip directions as needed, also if not moving, slowly come to a stop for smoothness
    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall) { FlipDirection(); }
        if (!damageable.LockVelocity)
        {
            if (CanMove) { rigidBod.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rigidBod.velocity.y); }
            else { rigidBod.velocity = new Vector2(Mathf.Lerp(rigidBod.velocity.x, 0, stopRate), rigidBod.velocity.y); }
        }
    }

    // Update is called once per frame
    //If there is a collision in the FOV, it has a target it can attack
    void Update()
    {
        HasTarget = fov.detectedCds.Count > 0;
        if (AttackCooldown > 0) { AttackCooldown -= Time.deltaTime; }
    }

    //If hit, take damage and be knocked back
    public void OnHit(int damage, Vector2 knockback)
    {
        //Multiply knockback by knockback resist, lower the value the more resistance it has
        rigidBod.velocity = new Vector2(knockback.x, rigidBod.velocity.y + knockback.y) * knockbackResist;
    }

    //If there's no ground detected while its walking (cliff nearby), flip directions
    //Note this does not occur when the target is not grounded, to prevent falling enemies from flipping over and over
    public void OnNoGrountDetected()
    {
        if (touchingDirections.IsGrounded) { FlipDirection(); }
    }
}