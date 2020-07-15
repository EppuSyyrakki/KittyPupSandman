using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBehaviour
{
    [HideInInspector] public GameObject owner;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public PlayerInputController player;


    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        owner = state.gameObject;
        rigidBody = owner.GetComponent<Rigidbody2D>();
        player = owner.GetComponent<PlayerInputController>();       
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        Debug.Log(player.LightDeployed);
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
    }

    public void InputBasedFlip(float horizontal)
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

    public void MoveRigidbody(float horizontal, float speed)
    {
        rigidBody.AddRelativeForce(new Vector2(horizontal * speed * Time.deltaTime, 0));

        if (Mathf.Abs(rigidBody.velocity.x) > player.maxSpeed)
        {
            rigidBody.velocity = new Vector2(player.maxSpeed * horizontal, rigidBody.velocity.y);
        }
    }
}
