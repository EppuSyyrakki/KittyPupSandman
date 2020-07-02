using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolOnGround : StateMachineBehaviour
{
    GameObject owner;
    private Transform[] waypoints;
    private int waypointIndex;
    private EnemyController controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        owner = animator.gameObject;
        controller = owner.GetComponent<EnemyController>();
        waypoints = controller.waypoints;
        waypointIndex = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float step = controller.speed * Time.deltaTime;
        owner.transform.position = Vector2.MoveTowards(owner.transform.position, waypoints[waypointIndex].position, step);

        if (owner.transform.position.x == waypoints[waypointIndex].position.x)
            NextWayPoint();            
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void NextWayPoint()
    {
        waypointIndex++;

        if (waypointIndex > waypoints.Length - 1)
            waypointIndex = 0;
    }

}
