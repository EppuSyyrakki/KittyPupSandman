using System.Collections;
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
        umbrellaClone = Instantiate(player.umbrella,
            player.umbrellaPosition.position,
            owner.transform.rotation,
            owner.transform);
        player.LookingDown = true;
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        MoveRigidbody(state.GetFloat("InputX") * 0.4f, player._fastSpeed);       
        rigidBody.drag = player._floatingDrag;
        InputBasedFlip(state.GetFloat("InputX"));
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
        rigidBody.drag = _originalDrag;
        Destroy(umbrellaClone);
        player.LookingDown = false;
    }
}
