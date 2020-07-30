using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacking : StateMachineBehaviour
{
    private BossController controller;

    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        controller = state.gameObject.GetComponent<BossController>();
        controller.attacking = true;
        controller.EnableColliders(true);
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
        controller.attacking = false;
        controller.EnableColliders(false);
    }

}
