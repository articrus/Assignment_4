using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 17-11-2024
 * This script controls the player's HP bar
 */
public class HPBarScript : MonoBehaviour
{
    public TMP_Text hpText;
    public Slider HPBar;
    public PlayerInfo playerInfo;

    //The player's HP is stored in the damageable component (can also make boss HP bars)
    Damageable playerDamage;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) { Debug.Log("ERROR: PLAYER NOT FOUND IN SCENE (MAY BE MISSING TAG"); }
        playerDamage = player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        HPBar.value = BarPercentage(playerDamage.HP, playerInfo.maxHP);
        hpText.text = $"HP {playerDamage.HP}/{playerInfo.maxHP}";
    }

    private void OnEnable()
    {
        playerDamage.hpChanged.AddListener(HPChanged);
    }

    private void OnDisable()
    {
        playerDamage.hpChanged.RemoveListener(HPChanged);
    }

    //The value returned will change the slider's position
    private float BarPercentage(float hp, float maxHp)
    {
        return hp/maxHp;
    }

    //When HP changes, update the bar and text accordigly
    private void HPChanged(int newHp, int maxHp)
    {
        HPBar.value = BarPercentage(newHp, maxHp);
        hpText.text = $"HP {newHp}/{maxHp}";
    }
}