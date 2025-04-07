using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 06-04-2025 | Last Modified: 06-04-2025
 * 
 * This script is used to apply speed boosts
 */
[CreateAssetMenu(menuName = "PowerUps/JumpBuff")]
public class JumpBuff : PowerUpEffect
{
    public float boostAmount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerController>().jumpPower *= boostAmount;
    }

    public override void Remove(GameObject target)
    {
        target.GetComponent<PlayerController>().jumpPower /= boostAmount;
    }
}