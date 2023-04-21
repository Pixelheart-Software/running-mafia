using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingAnimationBehaviour : StateMachineBehaviour
{
    private readonly int _isRollingAnimatorHash = Animator.StringToHash("IsRolling");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_isRollingAnimatorHash, true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_isRollingAnimatorHash, false);
    }
}
