using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boolcheck : StateMachineBehaviour
{
     [SerializeField] private string _boolName;
    [SerializeField] private bool _status;
    [SerializeField] private bool _resetOnExit = true;
    



    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //check if in animation not empty state
        animator.SetBool(_boolName, _status);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //reset 
        if (_resetOnExit)
            animator.SetBool(_boolName, !_status);
    }
  
}
