using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 09-11-2024 | Last Modified: 13-04-2025
 * 
 * This script is used to allow entities to attack each other
 */
public class Attack : MonoBehaviour
{
    //The damage an attack deals, can be adjusted in the inspector
    public float damage = 10;
    //The knockback an attack applies on hit
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if something can be hit
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            //Flip the knockback direction if the player is facing another direction
            Vector2 directionalKnock = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            //Hit the target
            damageable.Hit((int)damage, directionalKnock);
            //Debug.Log($"{collision.name} hit for {damage}");
        }
    }
}