using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 10f;
    [HideInInspector] public Animator state;

    void Start()
    {
        state = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
