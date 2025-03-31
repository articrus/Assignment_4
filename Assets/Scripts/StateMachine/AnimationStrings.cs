using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date): 07-12-2024
 * Names of strings for boolean and trigger parameters of the Animator
 * This is to prevent any potential errors when using string variables, since it is
 * very possible to type "isMoving" and "isMowing" and to not give any compilation errors
 */
public class AnimationStrings
{
    internal static string isMoving = "isMoving";
    internal static string isRunning = "isRunning";
    internal static string isGrounded = "isGrounded";
    internal static string yVelocity = "yVelocity";
    internal static string jumpTrigger = "jump";
    internal static string isOnWall = "isOnWall";
    internal static string isOnCeiling = "isOnCeiling";
    internal static string attack = "attack";
    internal static string canMove = "canMove";
    internal static string hasTarget = "hasTarget";
    internal static string spell = "spell";
    internal static string cutscene = "cutscene";
    internal static string isAlive = "isAlive";
    internal static string hit = "hit";
    internal static string lockVelocity = "lockVelocity";
    internal static string attackPause = "attackPause";
    internal static string sceneStart = "sceneStart";
    internal static string hasSpellBolt = "hasSpellBolt";
    internal static string fireboltDie = "fireboltDie";
    internal static string dashAttack = "dashAttack";
    internal static string dshAtk = "dshAtk";
    internal static string flameAtk = "flameAtk";
    internal static string flamesAttack = "flamesAttack";
    internal static string hover = "hover";
}