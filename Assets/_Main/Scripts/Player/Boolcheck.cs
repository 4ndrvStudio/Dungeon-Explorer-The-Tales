using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boolcheck : StateMachineBehaviour
{
     [SerializeField] private string _boolName;
    [SerializeField] private bool _status;
    [SerializeField] private bool _resetOnExit = true;
    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_boolName, _status);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_resetOnExit)
            animator.SetBool(_boolName, !_status);
    }
  
}
