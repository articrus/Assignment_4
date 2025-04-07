using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 06-04-2025 | Last Modified: 06-04-2025
 * 
 * This script is used to apply damage boosts
 */
[CreateAssetMenu(menuName = "PowerUps/DamageBuff")]
public class DamageBuff : PowerUpEffect
{
    public float boostAmount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().runSpeed *= boostAmount;
        target.GetComponent<PlayerController>().walkSpeed *= boostAmount;
    }

    public override void Remove(GameObject target)
    {
        target.GetComponent<PlayerController>().runSpeed /= boostAmount;
        target.GetComponent<PlayerController>().walkSpeed /= boostAmount;
    }
}
