using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 01-05-2025 | Last Modified: 01-05-2025
 * 
 * This script is used to store the player's upgrade tree progression
 */
[CreateAssetMenu(fileName = "UpgradeStorage", menuName = "UpgradeStorage")]
public class UpgradeStorage : ScriptableObject
{
    public UpgradeNode rootNode; //The root node and it's children are stored here
}