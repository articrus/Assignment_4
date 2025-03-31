using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This script is used to preserve Player data inbetween scenes
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
}