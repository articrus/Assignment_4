using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 09-11-2024, 2414537
 */
//This script allows dead enemies to fade out of the scene after a few seconds
public class DeathCleanup : StateMachineBehaviour
{
    //fadeTime is the amount of seconds it will take for the object to fade, can be adjusted in animator
    public float fadeTime = 0.5f;
    private float timeElapsed = 0.0f;

    //Necessary components
    SpriteRenderer sprite;
    GameObject toDelete;
    Color spriteColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0.0f;
        sprite = animator.GetComponent<SpriteRenderer>();
        toDelete = animator.gameObject;
        spriteColor = sprite.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;
        //Reduce the alpha of the sprite to make it transparent (eventually)
        sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, spriteColor.a * (1 - (timeElapsed / fadeTime)));
        if (timeElapsed > fadeTime) { Destroy(toDelete); }
    }
}