using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 12-11-2024, 2414537
 * This scipt was used originally as the attack area of the testnight, but can also be used to 
 * Detect cliffs and other objects by adjusting what the collisions repsond to
 */
public class FOV : MonoBehaviour
{
    public UnityEvent noCD;
    Collider2D cd;
    //To keep track of everything that is inside and was able to collide with the object
    public List<Collider2D> detectedCds = new List<Collider2D>();

    // Start is called before the first frame update
    private void Awake() { cd = GetComponent<Collider2D>(); }

    //On a detected trigger, add it to the list of triggers
    private void OnTriggerEnter2D(Collider2D collision) { detectedCds.Add(collision); }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCds.Remove(collision);
        if(detectedCds.Count <= 0) { noCD.Invoke(); }
    }
}