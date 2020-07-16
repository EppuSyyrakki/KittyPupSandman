﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloat : PlayerBehaviour
{
    private float _originalDrag;
    GameObject umbrellaClone;

    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        _originalDrag = rigidBody.drag;
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        MoveRigidbody(state.GetFloat("InputX") * 0.4f, player.fastSpeed);       
        rigidBody.drag = player.floatingDrag;
        InputBasedFlip(state.GetFloat("InputX"));
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
        rigidBody.drag = _originalDrag;
        Destroy(umbrellaClone);
    }
}