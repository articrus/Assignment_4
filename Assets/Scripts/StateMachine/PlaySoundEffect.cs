using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 012-11-2024, 2414537
 * This script is used to play sound effects after animations
 */
public class PlaySoundEffect : StateMachineBehaviour
{
    public AudioClip soundEffect;
    public float volume = 1.0f; //When adding the options, the volume should be increased/decreased by the options value
    public float delay = 0.25f;
    public bool playOnEnter = true, playOnExit = false, playOnDelay = false;

    private float timeSinceEnter = 0;
    private bool delayedPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter) { AudioSource.PlayClipAtPoint(soundEffect, animator.gameObject.transform.position, volume); }
        timeSinceEnter = 0f; delayedPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnDelay && !delayedPlayed)
        {
            timeSinceEnter += Time.deltaTime;
            if (timeSinceEnter > delay) 
            {
                AudioSource.PlayClipAtPoint(soundEffect, animator.gameObject.transform.position, volume); 
                delayedPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit) { AudioSource.PlayClipAtPoint(soundEffect, animator.gameObject.transform.position, volume); }
    }
}