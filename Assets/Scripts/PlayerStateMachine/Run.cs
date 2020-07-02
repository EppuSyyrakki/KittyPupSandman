using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : StateMachineBehaviour
{
    GameObject owner;
    Rigidbody2D rb;
    PlayerController pc;
    float lastXPosition;
    float acceleration;
    float maxSpeed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        owner = animator.gameObject;
        lastXPosition = owner.transform.position.x;
        pc = owner.GetComponent<PlayerController>();
        rb = owner.GetComponent<Rigidbody2D>();
        acceleration = pc.acceleration;
        maxSpeed = pc.maxSpeed;

        // Debug.Log(owner.name + " entered running mode");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        float horizontal = animator.GetFloat("Horizontal");
        MoveRigidbody(horizontal);
        InputBasedFlip(horizontal);
    }

    private void MoveRigidbody(float horizontal)
    {
        rb.AddRelativeForce(new Vector2(horizontal * acceleration * Time.deltaTime, 0));

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

        // Debug.Log(owner.name + " exiting running mode");
    }    
}
