using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This scipt is used to the boss door
 */
public class BossDoorScript : MonoBehaviour
{
    public PlayerInfo playerInfo;

    //If the player has both keys, face the boss
    void Start()
    {
        if(playerInfo.keyA == true && playerInfo.keyB == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
