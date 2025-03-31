using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This script is used to control the Key object
 */
public class KeyScript : MonoBehaviour
{
    //The number corresponding to which key effect/apperance, is changed in the inspector
    public int keyNum;
    public Sprite[] keys;
    SpriteRenderer spriteRend;
    Vector2 origin;

    private void Awake()
    {
        origin = transform.position;
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = keys[keyNum];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player)
        {
            //Spellbook will unlock different effects based on its number
            switch (keyNum)
            {
                //Key A
                case 0: player.keyA = true; break;
                //Key B
                case 1: player.keyB = true; break;
            }
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * 2f) * 0.25f + origin.y;
        transform.position = new Vector2(origin.x, newY);
    }
}