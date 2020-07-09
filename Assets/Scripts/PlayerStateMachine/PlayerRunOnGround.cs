using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunOnGround : PlayerBehaviour
{

    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        _graphic.SetBool("Walking", true);
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        MoveRigidbody(state.GetFloat("Horizontal"), _player.groundSpeed);
        HandleGraphics(state.GetFloat("Horizontal"));

        if (_owner.transform.position.y + 0.5f < _entryPoint.y) state.SetTrigger("Fall");           
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
        _graphic.SetBool("Walking", false);
        _graphic.SetBool("Running", false);
    }

    private void HandleGraphics(float horizontal)
    {
        InputBasedFlip(horizontal);
        float velocity = Mathf.Abs(_rigidBody.velocity.x);

        if (horizontal == 0)
        {
            _graphic.SetBool("Running", false);
            _graphic.SetBool("Walking", false);
        }
        else if (velocity > _player.runToWalkThreshold)
        {
            _graphic.SetBool("Running", true);
            _graphic.SetBool("Walking", false);
        }
        else if (velocity < _player.runToWalkThreshold)
        {
            _graphic.SetBool("Running", false);
            _graphic.SetBool("Walking", true);
        }        
    }
}
