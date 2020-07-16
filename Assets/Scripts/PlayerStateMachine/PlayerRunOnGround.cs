using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunOnGround : PlayerBehaviour
{ 
    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);        
    }    

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        DetermineCurrentSpeed();
        Debug.Log(player.currentSpeed);
        MoveRigidbody(state.GetFloat("InputX"), player.currentSpeed);
        InputBasedFlip(state.GetFloat("InputX"));
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
    }
    private void DetermineCurrentSpeed()
    {
        if (player.LightDeployed) player.currentSpeed = player.lightDeployedSpeed;
        else player.currentSpeed = player.fastSpeed;
    }
}
