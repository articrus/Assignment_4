using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This script is used to control the Spellbook object
 */
public class Spellbook : MonoBehaviour
{
    //The number corresponding to which spellbook effect/apperance, is changed in the inspector
    public int spellbookNum;
    //An array of sprites for the spellbook item
    public Sprite[] books;
    SpriteRenderer spriteRend;
    Vector2 origin;

    private void Awake()
    {
        origin = transform.position;
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = books[spellbookNum];
        //Spellbook will appear different based on its number (spellbook num) 1:Jump, 2:??, 3:Firebolt
    }

    //When the Player interacts with the spellbook, it applies the Permanent buff and destroys itself
    //The effect depends on the spellbook number
    //Enemies cannot interact with spellbooks since they are on the item layer
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player)
        {
            //Spellbook will unlock different effects based on its number
            switch (spellbookNum)
            {
                //Spellbook 1: Super Jump
                case 0: player.jumpBoost = 1.25f; player.hasSpellJump = true; break;
                //Spellbook 2: Freeze
                case 1: break;
                //Spellbook 3: Firebolt
                case 2: player.hasSpellBolt = true; player.GetComponent<Animator>().SetBool(AnimationStrings.hasSpellBolt, true); break;
                //Spellbook 4: Armor
                case 3: player.GetComponent<Damageable>().MaxHP = 150; player.GetComponent<Damageable>().Heal(50); break;
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame, used to make the object bob in place
    void Update()
    {
        float newY = Mathf.Sin(Time.time * 2f) * 0.25f + origin.y;
        transform.position = new Vector2(origin.x, newY);
    }
}