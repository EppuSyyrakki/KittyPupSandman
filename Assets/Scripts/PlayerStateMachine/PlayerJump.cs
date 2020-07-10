using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerBehaviour
{
    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        state.SetBool("Grounded", false);                                   
        _rigidBody.AddForce(new Vector2(0, _player.jumpForce));
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        MoveRigidbody(state.GetFloat("InputX"), _player.airborneSpeed);
        InputBasedFlip(state.GetFloat("InputX"));

        // if (_owner.transform.position.y < _entryPoint.y - 1 && !state.GetBool("Grounded")) state.SetTrigger("JumpToFall");
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
    }
}
