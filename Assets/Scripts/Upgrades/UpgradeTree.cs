using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 26-04-2025 | Last Modified: 01-05-2025
 * 
 * This script controls the upgrade tree
 */
public class UpgradeTree : MonoBehaviour
{
    public UpgradeNode rootNode;
    [SerializeField] private int chosenPath = -1; //-1 if not chosen yet
    [SerializeField] private bool canUnlock = true;
    [SerializeField] UpgradeStorage storage;

    //Creates the upgrade tree
    public void CreateUpgradeTree()
    {
        if(storage.rootNode == null) //If null, create new tree
        {
            //Root node
            rootNode = new UpgradeNode("Base Node", -1);
            //A path
            UpgradeNode pathA = new UpgradeNode("A", 0);
            UpgradeNode pathAA = new UpgradeNode("AA", 0);
            UpgradeNode pathAB = new UpgradeNode("AB", 3);
            //B path
            UpgradeNode pathB = new UpgradeNode("B", 1);
            UpgradeNode pathBA = new UpgradeNode("BA", 2);
            UpgradeNode pathBB = new UpgradeNode("BB", 1);

            //Add nodes to tree
            //Root children
            rootNode.Unlock();
            rootNode.children.Add(pathA);
            rootNode.children.Add(pathB);

            //A path children
            pathA.children.Add(pathAA);
            pathA.children.Add(pathAB);

            //B path children
            pathB.children.Add(pathBA);
            pathB.children.Add(pathBB);
        }
        else { rootNode = storage.rootNode; } //Use existing tree
    }

    //Unlock an upgrade
    public void UnlockUpgrade(int num)
    {
        //If we haven't gotten anything yet
        if(chosenPath == -1)
        {
            rootNode.children[num].Unlock();
            chosenPath = num;
            //Debug.Log($"Chosen path {num}");
        }
        //We are already on a chosen path, unlock the next node
        else if(canUnlock)
        {
            rootNode.children[chosenPath].children[num].Unlock();
            canUnlock = false; //No more upgrades
        }
    }

    /*
    //Using this for testing //Not anymore
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UnlockUpgrade(0);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            UnlockUpgrade(1);
        }
    }
    */

    //Save the tree data
    private void OnDestroy()
    {
        storage.rootNode = rootNode;
    }
}