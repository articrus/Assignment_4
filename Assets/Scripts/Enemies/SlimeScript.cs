using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 12-11-2024
 * This script is the code for the slime enemy
 */
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class SlimeScript : MonoBehaviour
{
    //Slime variables
    public float walkSpeed = 4.0f;
    public float stopRate = 0.05f;

    //Object Components
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

    public bool CanMove { get { return anim.GetBool(AnimationStrings.canMove); } }

    public bool LockVelocity
    {
        get { return anim.GetBool(AnimationStrings.lockVelocity); }
        set { anim.SetBool(AnimationStrings.lockVelocity, value); }
    }

    private void Awake()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Left) { WalkDirection = WalkableDirection.Right; }
        else if (WalkDirection == WalkableDirection.Right) { WalkDirection = WalkableDirection.Left; }
        else { }
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall) { FlipDirection(); }
        if (!damageable.LockVelocity)
        {
            if (CanMove) { rigidBod.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rigidBod.velocity.y); }
            else { rigidBod.velocity = new Vector2(Mathf.Lerp(rigidBod.velocity.x, 0, stopRate), rigidBod.velocity.y); }
        }
    }

    //If hit, take damage and be knocked back
    public void OnHit(int damage, Vector2 knockback)
    {
        //LockVelocity = true; see if needed during testing
        rigidBod.velocity = new Vector2(knockback.x, rigidBod.velocity.y + knockback.y);
    }

    //If there's no ground detected while its walking (cliff nearby), flip directions
    //Note this does not occur when the target is not grounded, to prevent falling enemies from flipping over and over
    public void OnNoGrountDetected()
    {
        if (touchingDirections.IsGrounded) { FlipDirection(); }
    }
}