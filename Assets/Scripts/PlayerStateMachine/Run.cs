using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : StateMachineBehaviour
{
    GameObject owner;
    [SerializeField] float _runningSpeed;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        owner = animator.gameObject;

        Debug.Log(owner.name + " entered running mode");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        // Possibly move the object via animation instead of this
        owner.transform.Translate(new Vector2(animator.GetFloat("Horizontal") * Time.deltaTime * _runningSpeed, 0 ));       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        Debug.Log(owner.name + " exiting running mode");
    }

    
}
