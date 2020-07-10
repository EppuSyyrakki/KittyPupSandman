using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBehaviour
{
    [HideInInspector] public GameObject _owner;
    [HideInInspector] public Rigidbody2D _rigidBody;
    [HideInInspector] public PlayerInputController _player;
    [HideInInspector] public Vector2 _entryPoint;

    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        _owner = state.gameObject;
        _rigidBody = _owner.GetComponent<Rigidbody2D>();
        _player = _owner.GetComponent<PlayerInputController>();
        _entryPoint = new Vector2(_owner.transform.position.x, _owner.transform.position.y);
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
            _owner.transform.localScale = new Vector3(1, 1, 1);
        }
        if (horizontal < 0)
        {
            _owner.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void MoveRigidbody(float horizontal, float speed)
    {
        _rigidBody.AddRelativeForce(new Vector2(horizontal * speed * Time.deltaTime, 0));

        if (Mathf.Abs(_rigidBody.velocity.x) > _player.maxSpeed)
        {
            _rigidBody.velocity = new Vector2(_player.maxSpeed * horizontal, _rigidBody.velocity.y);
        }
    }
}
