using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 13-04-2025 | Last Modified: 13-04-2025
 * 
 * This script is used to spawn potions/weapon upgrades throughout the levels
 */
public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> potionsToSpawn;
    [SerializeField] Transform potionSpawnPos;

    private void Awake()
    {
        if(potionSpawnPos != null)
        {
            Instantiate(potionsToSpawn[Random.Range(0, potionsToSpawn.Count)], potionSpawnPos.position, Quaternion.identity);
        }
    }
}