using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 12-11-2024, 2414537
 */
//This script allows the player to cast spells
public class SpellSpawner : MonoBehaviour
{
    public GameObject spell;
    public Transform spellSpawn;

    public void CastSpell()
    {
        GameObject newSpell = Instantiate(spell, spellSpawn.position, spell.transform.rotation);
        Vector3 scale = newSpell.transform.localScale;
        newSpell.transform.localScale = new Vector3(scale.x * transform.localScale.x > 0 ? 1 : -1, scale.y, scale.z);
    }
}
