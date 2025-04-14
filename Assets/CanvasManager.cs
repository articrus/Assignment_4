using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Written By: Gianni Coladonato
 * Date Created: 13-04-2025 | Last Modified: 13-04-2025
 * 
 * This script is used to manage the gamecanvas
 */
public class CanvasManager : MonoBehaviour
{
    [SerializeField] Image equippedWeapon;
    public static CanvasManager CMInstance;

    private void Awake()
    {
        if(CMInstance == null) { CMInstance = this; }
    }

    public void SetCurrentWeaponImage(Sprite weapon)
    {
        equippedWeapon.sprite = weapon;
    }
}