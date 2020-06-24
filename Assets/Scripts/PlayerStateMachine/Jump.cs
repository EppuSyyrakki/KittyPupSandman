using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    GameObject owner;

    [SerializeField] float _jumpingForce;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        owner = animator.gameObject;


        owner.transform.Translate(Vector2.up * Time.deltaTime * _jumpingForce);
        Debug.Log(owner.name + " entered jumping mode");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
        // do regular updates
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        animator.ResetTrigger("Jump");
        Debug.Log(owner.name + " has stopped jumping");
    }
}
