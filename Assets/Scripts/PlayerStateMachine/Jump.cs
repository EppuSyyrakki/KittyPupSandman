using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    GameObject owner;
    Rigidbody2D rb;
    PlayerController pc;
    [SerializeField] [Range(1, 8)] float _jumpingForceY = 3;
    [SerializeField] [Range(0.01f, 0.1f)] float _jumpingForceX = 0.01f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Grounded", false);
        owner = animator.gameObject;
        
        pc = owner.GetComponent<PlayerController>();
        pc.spriteRenderer.sprite = pc.jumpSprite;   // temporary tool before animations are added

        rb = owner.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(animator.GetFloat("Horizontal") * _jumpingForceX, _jumpingForceY));

        //Debug.Log(owner.name + " entered jumping mode, force: " + (_jumpingForceX));
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        float rotate = Input.GetAxis("Horizontal");

        //Debug.Log(rb.velocity.x);

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
