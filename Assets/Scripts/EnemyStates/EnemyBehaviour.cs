using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : StateMachineBehaviour
{
    [HideInInspector] public GameObject owner;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public EnemyController controller;
    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        owner = state.gameObject;
        rb = owner.GetComponent<Rigidbody2D>();
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
    }
}
