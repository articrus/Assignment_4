using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 30-12-2024 | Last Modified: 23-01-2025
 * 
 * This script is used as a base template for every powerup object
 */
public abstract class PowerUpEffect : ScriptableObject
{
    //Apply a powerup effect to a target (player)
    public abstract void Apply(GameObject target);
    //Remove a powerup effect to a target (player)
     public abstract void Remove(GameObject target);
}