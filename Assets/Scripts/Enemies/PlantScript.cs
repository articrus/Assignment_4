using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 12-11-2024, 2414537
 * This script is used to control the behavior of the Plant enemy
 */
[RequireComponent(typeof(Rigidbody2D), typeof(Damageable))]
public class PlantScript : MonoBehaviour
{
    //Plant variable
    public FOV fov;
    public bool _HasTarget = false;

    //Object Components
    Rigidbody2D rigidBod;
    Animator anim;
    Damageable damageable;

    public bool HasTarget
    {
        get { return _HasTarget; }
        private set
        {
            _HasTarget = value;
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public float AttackCooldown
    {
        get { return anim.GetFloat(AnimationStrings.attackPause); }
        private set { anim.SetFloat(AnimationStrings.attackPause, Mathf.Max(value, 0)); }
        //If less than zero set to 0 
    }

    //All damageables have LockVelocity, although this trait will be unused here
    public bool LockVelocity
    {
        get { return anim.GetBool(AnimationStrings.lockVelocity); }
        set { anim.SetBool(AnimationStrings.lockVelocity, value); }
    }

    private void Awake()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = fov.detectedCds.Count > 0;
        if (AttackCooldown > 0) { AttackCooldown -= Time.deltaTime; }
    }

    //If hit, take damage and be knocked back
    public void OnHit(int damage, Vector2 knockback)
    {
        //Plant does not move, remains still
        rigidBod.velocity = new Vector2(0, 0);
    }
}
