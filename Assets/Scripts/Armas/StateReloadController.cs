using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateReloadController : StateMachineBehaviour
{
    //Declaracion
    public ArmasDatos armasDatos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        armasDatos = GameObject.Find("Pistola").GetComponent<ArmasDatos>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.enabled = false;
        armasDatos.dispararPermitido = true;
        armasDatos.cambiarArma = true;
    }
}
