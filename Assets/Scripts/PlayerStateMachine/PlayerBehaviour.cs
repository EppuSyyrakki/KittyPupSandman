using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : StateMachineBehaviour
{
    [HideInInspector] public GameObject _owner;
    [HideInInspector] public Rigidbody2D _rb;
    [HideInInspector] public PlayerController _pc;
    [HideInInspector] public Animator _graphic;
    [HideInInspector] public Vector2 _entryPoint;
    public float groundSpeed { get; private set; }
    public float jumpForce { get; private set; }
    public float maxSpeed { get; private set; }
    public float airborneSpeed { get; private set; }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _owner = animator.gameObject;
        _rb = _owner.GetComponent<Rigidbody2D>();
        _pc = _owner.GetComponent<PlayerController>();
        _graphic = _owner.transform.GetChild(0).GetComponent<Animator>();
        _entryPoint = new Vector2(_owner.transform.position.x, _owner.transform.position.y);
        groundSpeed = _pc.groundSpeed;
        jumpForce = _pc.jumpForce;
        maxSpeed = _pc.maxSpeed;
        airborneSpeed = _pc.airborneSpeed;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    public void InputBasedFlip(float horizontal)
    {
        if (horizontal > 0)
        {
            _owner.transform.localScale = new Vector3(1, 1, 1);
        }
        if (horizontal < 0)
        {
            _owner.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void MoveRigidbody(float horizontal, float speed)
    {
        _rb.AddRelativeForce(new Vector2(horizontal * speed * Time.deltaTime, 0));

        if (Mathf.Abs(_rb.velocity.x) > maxSpeed)
        {
            _rb.velocity = new Vector2(maxSpeed * horizontal, _rb.velocity.y);
        }
    }
}
