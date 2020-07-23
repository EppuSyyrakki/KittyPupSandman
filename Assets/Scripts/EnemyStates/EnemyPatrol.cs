using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyBehaviour
{
    private Transform[] waypoints;
    private int waypointIndex;

    public override void OnStateEnter(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(state, stateInfo, layerIndex);
        waypoints = controller.waypoints;
        waypointIndex = 0;
    }

    public override void OnStateUpdate(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(state, stateInfo, layerIndex);
        float step = controller.speed * Time.deltaTime;

        if (controller.variableSpeed)
        {
            if (waypointIndex % 2 == 0)
                step *= 1.75f;
        }     
        owner.transform.position = Vector2.MoveTowards(owner.transform.position, waypoints[waypointIndex].position, step);

        if (RoughlySame(owner.transform.position.x, waypoints[waypointIndex].position.x))
            NextWayPoint();
    }

    public override void OnStateExit(Animator state, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(state, stateInfo, layerIndex);
    }

    private void NextWayPoint()
    {
        waypointIndex++;

        if (waypointIndex > waypoints.Length - 1)
            waypointIndex = 0;
    }

    bool RoughlySame(float a, float b)
    {
        return Mathf.Abs(a - b) < 0.1f;
    }
}
