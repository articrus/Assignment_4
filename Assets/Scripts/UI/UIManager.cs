using Assets.Scripts.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 * This script is used to manage the UI, specifically with the events relating to damage text, healing text, and tutorial text
 */
public class UIManager : MonoBehaviour
{
    public GameObject damageText;
    public GameObject healText;
    public List<GameObject> tutorialText;
    public Canvas canvas;

    private void Awake()
    {
        canvas = FindAnyObjectByType<Canvas>();
    }

    private void OnEnable()
    {
        CharaEvents.charaDamaged += TookDamage;
        CharaEvents.charaHealed += GotHealed;
        CharaEvents.passedTutorial += DisplayTutorial;
    }

    private void OnDisable()
    {
        CharaEvents.charaDamaged -= TookDamage;
        CharaEvents.charaHealed -= GotHealed;
        CharaEvents.passedTutorial -= DisplayTutorial;
    }

    //Can display the exact damage taken
    public void TookDamage(GameObject chara, int damage)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(chara.transform.position);
        TMP_Text newText = Instantiate(damageText, spawnPos, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        newText.text = $"-{damage.ToString()}";
    }

    //Can display the exact amount healed
    public void GotHealed(GameObject chara, int heal) 
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(chara.transform.position);
        TMP_Text newText = Instantiate(healText, spawnPos, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        newText.text = $"+{heal.ToString()}";
    }

    public void DisplayTutorial(GameObject chara, int tutorNum)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(chara.transform.position);
        TMP_Text newText = Instantiate(tutorialText[tutorNum], spawnPos, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
    }
}