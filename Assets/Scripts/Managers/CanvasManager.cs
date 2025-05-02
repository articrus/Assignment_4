using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
 * Written By: Gianni Coladonato
 * Date Created: 13-04-2025 | Last Modified: 01-05-2025
 * 
 * This script is used to manage the gamecanvas
 */
public class CanvasManager : MonoBehaviour
{
    [SerializeField] Image equippedWeapon;
    public static CanvasManager CMInstance;
    [SerializeField] GameObject pausePanel;
    //Upgrade tree
    public UpgradeTree upgradeTree;
    [SerializeField] GameObject nodePrefab;
    [SerializeField] RectTransform rectTransform;
    private float xSpacing = 250f;
    private float ySpacing = 75f;
    [SerializeField] UpgradeStorage storage;
    private void Awake()
    {
        if (CMInstance == null) { CMInstance = this; }
        upgradeTree = GetComponent<UpgradeTree>();
        StartCoroutine(InitTree()); //Draw the pause menu's trees
    }

    //Create the tree, draw all nodes, and check if they're active
    private IEnumerator InitTree()
    {
        upgradeTree.CreateUpgradeTree();
        yield return null;
        DrawTree(upgradeTree.rootNode, Vector2.zero, rectTransform);
        yield return null;
        CheckTree(upgradeTree.rootNode);
        yield return null;
        pausePanel.SetActive(false);
    }

    //Update which weapon is active
    public void SetCurrentWeaponImage(Sprite weapon)
    {
        equippedWeapon.sprite = weapon;
    }

    //Pauses/Unpauses the game
    public void TogglePause()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1; //Resume game
        }
        else
        {
            pausePanel.SetActive(true);
            CheckTree(upgradeTree.rootNode); //Check the tree if it's been updated
            Time.timeScale = 0;
        }
    }

    //Draws a visual representation of the player's upgrade tree
    private void DrawTree(UpgradeNode node, Vector2 position, Transform parent)
    {
        GameObject nodeUI = Instantiate(nodePrefab, parent);
        nodeUI.GetComponent<RectTransform>().anchoredPosition = position;
        nodeUI.GetComponentInChildren<TextMeshProUGUI>().text = node.text;
        node.nodeUI = nodeUI;
        //Get the total width of the current branch
        float totalWidth = GetChildrenWidth(node);
        float currentX = position.x - totalWidth / 2f;

        foreach (var child in node.children)
        {
            float childWidth = GetChildrenWidth(child);
            Vector2 childPos = new Vector2(currentX + childWidth / 2f, position.y - ySpacing);
            DrawTree(child, childPos, parent);
            currentX += childWidth; //Increment so children don't get set to same position
        }
    }

    //Returns a float used to space out the child nodes evenly
    private float GetChildrenWidth(UpgradeNode node)
    {
        if(node.children.Count == 0) { return xSpacing; } //Single node
        float width = 0f;
        foreach(UpgradeNode child in node.children)
        {
            width += GetChildrenWidth(child);
        }
        return Mathf.Max(width, xSpacing);
    }

    //Cycle through the tree to check if nodes have been unlocked
    private void CheckTree(UpgradeNode node)
    {
        if (node.nodeUI != null)
        {
            Image image = node.nodeUI.GetComponent<Image>();
            if (node.isUnlocked)
            {
                image.color = Color.white; //If it's unlocked, color it
            }
            else
            {
                image.color = Color.gray; //Else, gray it out
            }
        }
        foreach (var child in node.children)
        {
            CheckTree(child); //Check all subsequent children of this node
        }
    }

}