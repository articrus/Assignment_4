using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 09-11-2024, 2414537
 */
//This script sets boolean behaior of the animator parameters, for example, preventing movement on an animation or state
public class SetBooleanBehavior : StateMachineBehaviour
{
    public string boolName;
    public bool updateOnStateMachine;
    public bool updateOnState;
    public bool valueOnEnter, valueOnExit;

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine) { animator.SetBool(boolName, valueOnEnter); }
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine) { animator.SetBool(boolName, valueOnExit); }
    }

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState) { animator.SetBool(boolName, valueOnEnter); }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState) { animator.SetBool(boolName, valueOnExit); }
    }
}