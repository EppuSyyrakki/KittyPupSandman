using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    GameObject owner;
    float _jumpingForceY = 3;
    float _jumpingForceX = 2;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        owner = animator.gameObject;
        animator.SetBool("Grounded", false);

        owner = animator.gameObject;
        Rigidbody2D rb = owner.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(animator.GetFloat("Horizontal") * _jumpingForceX, _jumpingForceY));

        // Debug.Log(owner.name + " entered jumping mode");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Debug.Log(owner.name + " is in the air");
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.ResetTrigger("Jump");
        
        // Debug.Log(owner.name + " has stopped jumping");
    }
}
