using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * Date Created: 27-04-2025 | Last Modified: 27-04-2025
 * 
 * This script controls the upgrade point object
 */
public class UpgradePoint : MonoBehaviour
{
    public int upgradeValue; //Either 0 or 1
    SpriteRenderer spriteRenderer;

    //To hover up and down
    Vector2 origin;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        origin = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (upgradeValue)
        {
            case 0: spriteRenderer.color = Color.red; break;
            case 1: spriteRenderer.color = Color.blue; break;
            default: spriteRenderer.color = Color.green; break; //indicator of error
        }
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * 2f) * 0.25f + origin.y;
        transform.position = new Vector2(origin.x, newY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player)
        {
            CanvasManager.CMInstance.upgradeTree.UnlockUpgrade(upgradeValue);
            Destroy(gameObject);
        }
    }
}
