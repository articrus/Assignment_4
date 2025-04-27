using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 08-12-2025 | Last Modified: 27-04-2025
 * 
 * This script is used to store player info between scenes
 */
[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Persistance")]
public class PlayerInfo : ScriptableObject
{
    public int currentHP;
    public int maxHP;
    public float jumpBoost;
    public bool hasSpellJump;
    public bool hasSpellBolt;
    public Vector2[] spawnPositions;
    public bool keyA;
    public bool keyB;
    public List<GameObject> weapons;
    public float atkBoost;
    public float spdBoost;
}