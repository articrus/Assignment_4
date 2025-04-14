using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 13-04-2025 | Last Modified: 13-04-2025
 * 
 * This script is used to boost/manage the player's damage
 */
public class PlayerDamage : MonoBehaviour
{
    Attack meleeOne;
    Attack meleeTwo;

    private void Awake()
    {
        meleeOne = transform.GetChild(0).GetComponent<Attack>();
        meleeTwo = transform.GetChild(1).GetComponent<Attack>();
    }

    public void ChangeWeaponStats(WeaponStats stats)
    {
        meleeOne.damage = stats.attackDamages.x;
        meleeOne.knockback = stats.firstAttack;
        meleeTwo.damage = stats.attackDamages.y;
        meleeTwo.knockback = stats.secondAttack;
    }

    public void ApplyDamageBoost(float boost)
    {
        meleeOne.damage *= boost;
        meleeTwo.damage *= boost;
    }

    public void RemoveDamageBoost(float boost)
    {
        meleeOne.damage /= boost;
        meleeTwo.damage /= boost;
    }

    public void ApplyKnockbackBoost(float boost)
    {
        meleeOne.knockback *= boost;
        meleeTwo.knockback *= boost;
    }

    public void RemoveKnockbackBoost(float boost)
    {
        meleeOne.knockback /= boost;
        meleeTwo.knockback /= boost;
    }
}