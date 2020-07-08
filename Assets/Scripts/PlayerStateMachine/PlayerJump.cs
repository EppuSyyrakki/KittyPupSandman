using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerBehaviour
{
    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        state.SetBool("Grounded", false);
        _graphic.SetBool("Jumping", true);                       
        _rigidBody.AddForce(new Vector2(0, _player.jumpForce));
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        MoveRigidbody(state.GetFloat("Horizontal"), _player.airborneSpeed);
        InputBasedFlip(state.GetFloat("Horizontal"));
        Debug.Log(_entryPoint);
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
        _graphic.SetBool("Jumping", false);
    }
}
