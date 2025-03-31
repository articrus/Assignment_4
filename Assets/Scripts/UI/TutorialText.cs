using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 08-12-2024
 */
public class TutorialText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 90, 0);
    public float timeToFade = 0.75f;
    private float timeElapsed = 0.0f;
    private Color originColor;

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        originColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if (timeElapsed < timeToFade)
        {
            textMeshPro.color = new Color(originColor.r, originColor.g, originColor.b, originColor.a * (1 - (timeElapsed / timeToFade)));
        }
        else { Destroy(gameObject); }
    }
}
