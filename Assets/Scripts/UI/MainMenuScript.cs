using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
/*
 * Written By: Gianni Coladonato
 * Date Created: 17-12-2024 | Last Modified: 01-05-2025
 * 
 * This scipt is used to control the main menu components
 */
public class MainMenuScript : MonoBehaviour
{
    //Needed components
    public TextMeshProUGUI title;
    public float floatSpeed = 2.0f;
    public AudioSource audioSc;
    public LevelLoader loader;
    public PlayerInfo playerInfo;
    public OptionsSettings settings;
    [SerializeField] UpgradeStorage storage;

    //ToGetComponents
    Vector3 hovering;
    RectTransform originTitle; //The original position of the title

    private void Awake()
    {
        originTitle = title.rectTransform;
    }

    //Starts the game
    public void PlayButton()
    {
        audioSc.Play();
        setDefaultPlayerInfo();
        loader.LoadNextLvl(1);
    }

    //Reset the player's stats
    void setDefaultPlayerInfo()
    {
        playerInfo.currentHP = 100;
        playerInfo.maxHP = 100;
        playerInfo.jumpBoost = 1.0f;
        playerInfo.hasSpellJump = false;
        playerInfo.hasSpellBolt = false;
        playerInfo.spawnPositions = new Vector2[15];
        playerInfo.keyA = false;
        playerInfo.keyB = false;
        playerInfo.atkBoost = 1;
        playerInfo.spdBoost = 1;
        storage.rootNode = null; //Reset player's upgrade progression
    }

    //Quits the game
    public void QuitButton()
    {
        Application.Quit();
    }

    //Open the options menu to adjust the music and sfx volume
    public void OptionsButton()
    {
        Debug.Log("Options selected!");
        //Add code here
    }

    // Update is called once per frame
    void Update()
    {
        //float newY = Mathf.Sin(Time.time * 2f) * 0.25f + origin.y;\
        hovering = new Vector3(0, (Mathf.Sin(Time.time * 2f) * floatSpeed), 0);
        originTitle.position += hovering * Time.deltaTime;
    }
}