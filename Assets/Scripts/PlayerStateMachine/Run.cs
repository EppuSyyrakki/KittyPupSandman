using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : StateMachineBehaviour
{
    GameObject owner;
    Rigidbody2D rb;
    PlayerController pc;
    float lastXPosition;
    [SerializeField][Range(1,8)] float _runningSpeed = 5;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        owner = animator.gameObject;
        lastXPosition = owner.transform.position.x;
        pc = owner.GetComponent<PlayerController>();
        pc.spriteRenderer.sprite = pc.runSprite;   // temporary tool before animations are added

        rb = owner.GetComponent<Rigidbody2D>();

        // Debug.Log(owner.name + " entered running mode");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (lastXPosition > owner.transform.position.x && !pc.spriteRenderer.flipX)      // if moving left, flip sprite X
            pc.spriteRenderer.flipX = true;

        if (lastXPosition < owner.transform.position.x && pc.spriteRenderer.flipX)       // if moving right, don't flip X
            pc.spriteRenderer.flipX = false;

        // Possibly move the object via animation instead of this
        owner.transform.Translate(new Vector2(animator.GetFloat("Horizontal") * Time.deltaTime * _runningSpeed, 0));       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        // Debug.Log(owner.name + " exiting running mode");
    }    
}
