using Assets.Scripts.Events;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.Events;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 17-11-2024
 * This script allows Players and Enemies to be damaged
 */
public class Damageable : MonoBehaviour
{
    //To let game components respond to hits
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> hpChanged;
    //Max HP is the player's maximum hp, while HP is the player's current hp
    [SerializeField]
    private int _maxHP = 100;
    [SerializeField]
    private int _hp = 50;
    //Is invincible triggers after taking damage, making the player invincible for a number of
    //seconds equal to the iFrames value
    private bool _isAlive = true;
    private bool _isInvincible = false;
    private float timeHit = 0.0f;
    public float iFrames = 0.25f;
    Animator anim;

    //Check if entity is alive
    public bool isAlive
    {
        get { return _isAlive; } 
        set 
        {  
            _isAlive = value; 
            anim.SetBool(AnimationStrings.isAlive, value);
        }  
    }
    //Check if entity is invincibles
    private bool isInvincible { get { return _isInvincible; } set { _isInvincible = value; } }

    //The max hp of an entity
    public int MaxHP { get { return _maxHP; } set { _maxHP = value; } }

    //The current hp of an entity
    public int HP
    {
        get { return _hp; }
        set 
        { 
            _hp = value;
            hpChanged?.Invoke(_hp, MaxHP);
            //If player has 0 hp or less, die
            if(_hp <= 0) { isAlive = false; }
        }
    }

    //LockVelocity happens when an entity is hit, triggering the hit animation and knocking the entity back,
    //knockback temporarily disables movement
    public bool LockVelocity { 
        get { return anim.GetBool(AnimationStrings.lockVelocity); }
        set { anim.SetBool(AnimationStrings.lockVelocity, value); } 
    }

    private void Awake() { anim = GetComponent<Animator>(); }

    public void Update()
    {
        //Remove invincibility a few seconds equal to the iFrames count
        if (isInvincible) 
        {
            if(timeHit > iFrames)
            {
                isInvincible = false;
                timeHit = 0;
            }
            timeHit += Time.deltaTime;
        }
    }

    //Hit a target for damage and apply some knockback
    public void Hit(int damage, Vector2 knockback)
    {
        if(isAlive && !isInvincible)
        {
            HP -= damage;
            isInvincible = true;
            LockVelocity = true;   
            anim.SetTrigger(AnimationStrings.hit);
            //This allows other components of gameobjects to respond to a hit since we're using a unity event
            damageableHit?.Invoke(damage, knockback);
            CharaEvents.charaDamaged.Invoke(gameObject, damage);
        }
    } 

    //Heal an entity, does not heal above the HP maximum
    public void Heal(int hpHeal)
    {
        if (isAlive)
        {
            //Cant heal above HP max
            int maxHeal = Mathf.Max(MaxHP - HP, 0);
            int hpHealed = Mathf.Min(maxHeal, hpHeal);
            HP += hpHealed;
            CharaEvents.charaHealed.Invoke(gameObject, hpHealed);
        }
    }
}