using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBehaviour
{
    [HideInInspector] public GameObject owner;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public PlayerInputController player;
    private float _realMaxSpeed;

    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        owner = state.gameObject;
        rigidBody = owner.GetComponent<Rigidbody2D>();
        player = owner.GetComponent<PlayerInputController>();
        _realMaxSpeed = player._maxSpeed / 3;
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
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

        if (Mathf.Abs(rigidBody.velocity.x) > _realMaxSpeed)
        {
            rigidBody.velocity = new Vector2(_realMaxSpeed * horizontal, rigidBody.velocity.y);
        }
    }
}
