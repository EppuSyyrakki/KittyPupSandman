using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10f;
    [HideInInspector] public Animator state;
    
    // These are set in LightWeapon.cs if this is hit by the Light trigger
    [HideInInspector] public bool escaping;
    [HideInInspector] public bool dying;
    [HideInInspector] public Vector2 escapeDirection;

    void Start()
    {
        state = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        state.SetBool("Escaping", escaping);
        state.SetBool("Dying", dying);
    }
}
