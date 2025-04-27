using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 26-04-2025 | Last Modified: 27-04-2025
 * 
 * This script defines the upgrade tree's nodes
 */
[System.Serializable]
public class UpgradeNode
{
    public bool isUnlocked = false;
    public string upgradeName;
    public int upgradePath;
    public GameObject nodeUI; //Stored for later, used in visualization
    public List<UpgradeNode> children = new List<UpgradeNode>();

    public UpgradeNode(string name, int path)
    {
        upgradeName = name;
        upgradePath = path;
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            switch (upgradePath)
            {
                case 0: IncreasePower(); Debug.Log($"Increased Power"); break;
                case 1: IncreaseSpeed(); Debug.Log($"Increased Speed"); break;
                case 2: IncreaseJump(); Debug.Log($"Increased Jump"); break;
                case 3: IncreaseHP(); Debug.Log($"Increased HP"); break;
                default: break; //Do nothing
            }
            Debug.Log($"Unlocked {upgradeName}!");
        }
    }

    public void IncreasePower()
    {
        PlayerController.PCInstance.AttackBoost = 1.25f;
    }

    public void IncreaseSpeed()
    {
        PlayerController.PCInstance.spdBoost += 0.25f;
    }

    public void IncreaseJump()
    {
        PlayerController.PCInstance.jumpBoost++;
    }

    public void IncreaseHP()
    {
        PlayerController.PCInstance.damageable.MaxHP += 5;
        PlayerController.PCInstance.damageable.HP += 5;
    }
}