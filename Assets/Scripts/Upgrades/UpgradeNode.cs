using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 26-04-2025 | Last Modified: 01-05-2025
 * 
 * This script defines the upgrade tree's nodes
 */
[System.Serializable]
public class UpgradeNode
{
    public bool isUnlocked = false;
    public string upgradeName;
    public int upgradePath;
    public string text;
    public GameObject nodeUI; //Stored for later, used in visualization
    public List<UpgradeNode> children = new List<UpgradeNode>();

    public UpgradeNode(string name, int path)
    {
        upgradeName = name;
        upgradePath = path;
        //Used by the skill tree, sets the visual text later on for what boost is given
        switch (upgradePath)
        {
            case 0: text = "Power"; break;
            case 1: text = "Speed"; break;
            case 2: text = "Jump"; break;
            case 3: text = "HP"; break;
            default: text = ""; break; //No text / base node
        }
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            //Unlock the upgrade based on it's upgrade path
            switch (upgradePath)
            {
                case 0: IncreasePower(); break;
                case 1: IncreaseSpeed(); break;
                case 2: IncreaseJump(); break;
                case 3: IncreaseHP(); break;
                default: break; //Do nothing
            }
            //Debug.Log($"Unlocked {upgradeName}!");
        }
    }

    //Increase the player's power by a slight amount
    public void IncreasePower()
    {
        PlayerController.PCInstance.AttackBoost = 1.25f;
    }

    //Increase the player's speed by a slight amount
    public void IncreaseSpeed()
    {
        PlayerController.PCInstance.spdBoost += 0.5f;
    }

    //Increase the player's jump power by a slight amount
    public void IncreaseJump()
    {
        PlayerController.PCInstance.jumpBoost++;
    }

    //Increase the player's HP by a slight amount
    public void IncreaseHP()
    {
        PlayerController.PCInstance.damageable.MaxHP += 25;
        PlayerController.PCInstance.damageable.HP += 25;
    }
}