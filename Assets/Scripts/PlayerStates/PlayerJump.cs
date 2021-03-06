﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerBehaviour
{
    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        state.SetBool("Grounded", false);                                   
        rigidBody.AddForce(new Vector2(0, player._jumpForce));
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        MoveRigidbody(state.GetFloat("InputX"), player._slowSpeed);
        InputBasedFlip(state.GetFloat("InputX"));
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
    }
}
