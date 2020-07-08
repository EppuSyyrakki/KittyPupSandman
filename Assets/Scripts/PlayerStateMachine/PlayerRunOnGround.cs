using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
        float horizontal = state.GetFloat("Horizontal");
        HandleGraphics(horizontal);
        MoveRigidbody(horizontal, _player.groundSpeed);
        InputBasedFlip(horizontal);
        
        if (!state.GetBool("Grounded"))
            state.SetTrigger("Falling");
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
    }

    private void HandleGraphics(float horizontal)
    {
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
