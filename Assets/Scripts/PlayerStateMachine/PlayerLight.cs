using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : PlayerBehaviour
{
    private GameObject lampClone;

    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);

        if (state.GetBool("InputLight") && !player.LightDeployed)
        {
            lampClone = Instantiate(player.lamp,
                player.lampPosition.position,
                owner.transform.rotation,
                owner.transform);
            player.LightDeployed = true;
        }

        if (!state.GetBool("InputLight") && player.LightDeployed)
        {
            Destroy(lampClone);
            player.LightDeployed = false;
        }
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
    }
}
