using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 17-11-2024
 * This script controls the hpItems
 */
public class HealthItem : MonoBehaviour
{
    //The amount of HP healed by a healing item, the default is 20 but can be changed in the inspector
    public int HPHealed = 20;
    //The original position of the object in the scene
    Vector2 origin;

    private void Awake()
    {
        origin = transform.position;
    }

    //This method allows the item to bob up and down while remaining in the same x position
    private void Update()
    {
        float newY = Mathf.Sin(Time.time * 2f) * 0.25f + origin.y;
        transform.position = new Vector2(origin.x, newY);
    }

    //When a player (which is a damageable) interacts with the item, it heals the player and destroys itself
    //Note: enemies will not be able to interact with this collision since the Item layer only interacrs with Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable)
        {
            damageable.Heal(HPHealed);
            Destroy(gameObject);
        }
    }
}