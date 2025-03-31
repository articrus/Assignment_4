using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 07-12-2024
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