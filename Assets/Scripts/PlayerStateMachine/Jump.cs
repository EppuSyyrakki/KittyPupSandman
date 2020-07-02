using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    GameObject owner;
    Rigidbody2D rb;
    PlayerController pc;
    float jumpForce;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Grounded", false);
        owner = animator.gameObject;       
        pc = owner.GetComponent<PlayerController>();
        jumpForce = pc.jumpForce;
        rb = owner.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, jumpForce));

        pc.spriteRenderer.sprite = pc.jumpSprite;   // temporary tool before animations are added
        //Debug.Log(owner.name + " entered jumping mode, force: " + (_jumpingForceX));
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        float rotate = Input.GetAxis("Horizontal");

        Flip(rotate);       
    }

    private void Flip(float rotate)
    {
        if (rotate < 0)
            FlipLeft();

        if (rotate > 0)
            FlipRight();
    }

    private void FlipRight()
    {
        if (pc.spriteRenderer.flipX)        
            pc.spriteRenderer.flipX = false;
    }

    private void FlipLeft()
    {
        if (!pc.spriteRenderer.flipX)
            pc.spriteRenderer.flipX = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.ResetTrigger("Jump");
    }
}
