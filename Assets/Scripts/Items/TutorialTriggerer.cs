using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This script is used to invoke a Character event to display tutorial info
 */
public class TutorialTriggerer : MonoBehaviour
{
    public int tutorialNum;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player)
        {
            CharaEvents.passedTutorial.Invoke(gameObject, tutorialNum);
        }
    }
}
