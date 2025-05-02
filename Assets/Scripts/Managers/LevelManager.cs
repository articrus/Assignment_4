using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 13-04-2025 | Last Modified: 01-05-2025
 * 
 * This script is used to spawn potions/weapon upgrades throughout the levels
 */
public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> potionsToSpawn;
    [SerializeField] Transform potionSpawnPos;
    [SerializeField] Transform upgradePointPos;
    [SerializeField] private GameObject upgradePoint;
    [SerializeField] private int scenePointValue;
    private void Awake()
    {
        //If the potion spawn is not null, spawn a potion
        if(potionSpawnPos != null)
        {
            Instantiate(potionsToSpawn[Random.Range(0, potionsToSpawn.Count)], potionSpawnPos.position, Quaternion.identity);
        }
        //If the upgrade point spawn is not null, spawn an upgrade point
        if (upgradePointPos != null)
        {
            GameObject point = Instantiate(upgradePoint, upgradePointPos.position, Quaternion.identity);
            point.GetComponent<UpgradePoint>().upgradeValue = scenePointValue; //Value is dependant on location / scene
        }
    }
}