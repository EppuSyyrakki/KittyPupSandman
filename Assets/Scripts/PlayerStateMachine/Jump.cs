using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    GameObject owner;
    Rigidbody2D rb;
    PlayerController pc;
    float jumpForce;
    float maxSpeed;
    float airborneSpeed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Grounded", false);
        owner = animator.gameObject;       
        pc = owner.GetComponent<PlayerController>();
        jumpForce = pc.jumpForce;
        maxSpeed = pc.maxSpeed;
        airborneSpeed = pc.airborneSpeed;
        
        rb = owner.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, jumpForce));

        //Debug.Log(owner.name + " entered jumping mode, force: " + (_jumpingForceX));
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        float horizontal = Input.GetAxis("Horizontal");
        MoveRigidbody(horizontal);
        InputBasedFlip(horizontal);
    }

    private void MoveRigidbody(float horizontal)
    {
        rb.AddRelativeForce(new Vector2(horizontal * airborneSpeed * Time.deltaTime, 0));

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed * horizontal, rb.velocity.y);
        }
    }

    private void InputBasedFlip(float horizontal)
    {
        if (horizontal > 0)
        {
            owner.transform.localScale = new Vector3(1, 1, 1);
        }
        if (horizontal < 0)
        {
            owner.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.ResetTrigger("Jump");
    }
}
