using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolOnGround : StateMachineBehaviour
{
    GameObject _owner;
    Rigidbody2D _rb;
    private Transform[] waypoints;
    private int waypointIndex;
    private EnemyController controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _owner = animator.gameObject;
        _rb = _owner.GetComponent<Rigidbody2D>();
        controller = _owner.GetComponent<EnemyController>();
        waypoints = controller.waypoints;
        waypointIndex = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float step = controller.speed * Time.deltaTime;

        if (waypointIndex % 2 == 0)
            step *= 1.75f;

        _owner.transform.position = Vector2.MoveTowards(_owner.transform.position, waypoints[waypointIndex].position, step);

        if (RoughlySame(_owner.transform.position.x, waypoints[waypointIndex].position.x))
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

    bool RoughlySame(float a, float b)
    {
        return Mathf.Abs(a - b) < 0.01f;
    }
}
