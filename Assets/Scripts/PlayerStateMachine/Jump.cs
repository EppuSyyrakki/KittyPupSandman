using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : StateMachineBehaviour
{
    GameObject owner;
    Rigidbody2D rb;
    PlayerController pc;
    [SerializeField] [Range(1, 8)] float _jumpingForceY = 3;
    [SerializeField] [Range(1, 8)] float _jumpingForceX = 2;    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Grounded", false);
        owner = animator.gameObject;
        
        pc = owner.GetComponent<PlayerController>();
        pc.spriteRenderer.sprite = pc.jumpSprite;   // temporary tool before animations are added

        rb = owner.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(animator.GetFloat("Horizontal") * _jumpingForceX, _jumpingForceY));

        // Debug.Log(owner.name + " entered jumping mode");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        float rotation = Input.GetAxis("Horizontal"); 
        Debug.Log(rb.velocity.x);

        if ((rb.velocity.x < 0 || rotation < 0) && !pc.spriteRenderer.flipX )      // if moving left, flip sprite X
            pc.spriteRenderer.flipX = true;

        if ((rb.velocity.x > 0 || rotation > 0) && pc.spriteRenderer.flipX)       // if moving right, don't flip X
            pc.spriteRenderer.flipX = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.ResetTrigger("Jump");
    }
}
