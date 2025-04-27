using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Written By: Gianni Coladonato
 * Date Created: 13-04-2025 | Last Modified: 27-04-2025
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

    private void Awake()
    {
        if (CMInstance == null) { CMInstance = this; }
        upgradeTree = GetComponent<UpgradeTree>();
        StartCoroutine(InitTree());
    }

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

    public void SetCurrentWeaponImage(Sprite weapon)
    {
        equippedWeapon.sprite = weapon;
    }

    public void TogglePause()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            CheckTree(upgradeTree.rootNode);
            Time.timeScale = 0;
        }
    }

    private void DrawTree(UpgradeNode node, Vector2 position, Transform parent)
    {
        GameObject nodeUI = Instantiate(nodePrefab, parent);
        nodeUI.GetComponent<RectTransform>().anchoredPosition = position;
        node.nodeUI = nodeUI; //Store reference
        float childPosX = position.x - (node.children.Count - 1) * xSpacing / 2;

        for(int i = 0; i < node.children.Count; i++)
        {
            Vector2 childPos = new Vector2(childPosX + i * xSpacing, position.y - ySpacing);
            DrawTree(node.children[i], childPos, parent);
        }
    }

    private void CheckTree(UpgradeNode node)
    {
        if (node.nodeUI != null)
        {
            Image image = node.nodeUI.GetComponent<Image>();
            if (node.isUnlocked)
            {
                image.color = Color.white;
            }
            else
            {
                image.color = Color.gray;
            }
        }
        foreach(var child in node.children)
        {
            CheckTree(child);
        }
    }
}