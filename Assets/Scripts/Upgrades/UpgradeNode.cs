using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 26-04-2025 | Last Modified: 26-04-2025
 * 
 * This script defines the upgrade tree's nodes
 */
[System.Serializable]
public class UpgradeNode
{
    public bool isUnlocked = false;
    public string upgradeName;
    public int upgradePath;
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
                case 0: IncreasePower(); break;
                case 1: IncreaseSpeed(); break;
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
        PlayerController.PCInstance.spdBoost++;
    }
}